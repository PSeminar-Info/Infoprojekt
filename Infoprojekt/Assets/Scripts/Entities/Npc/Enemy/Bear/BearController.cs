using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace Entities.Npc.Enemy.Bear
{
    // Bear is great! Bear walks, bear stands, bear sleeps, bear sits, bear attacks. Bear very powerful.
    // @author Bjoern
    public class BearController : Npc
    {
        // Animations
        private static readonly int Attack1 = Animator.StringToHash("Attack1");
        private static readonly int Attack2 = Animator.StringToHash("Attack2");
        private static readonly int Attack3 = Animator.StringToHash("Attack3");
        private static readonly int Attack4 = Animator.StringToHash("Attack5");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int RunForward = Animator.StringToHash("Run");
        private static readonly int WalkForward = Animator.StringToHash("WalkForward");
        private static readonly int WalkBackward = Animator.StringToHash("WalkBackward");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Roar = Animator.StringToHash("Buff");
        private static readonly int Sit = Animator.StringToHash("Sit");
        private static readonly int Stand = Animator.StringToHash("Idle");
        private static readonly int Sleep_A = Animator.StringToHash("Sleep");
        private int _currentAnimation = Idle;

        [Header("Combat")]
        public float attackCooldown = 1f;
        public float attackRange = 5f;
        public float activationRange = 60f;
        public float rotationSpeed = 10f;
        public GameObject player;
        public int Health;

        [Header("Movement & Cooldowns")]
        // variables responsible for movement and general behaviour 
        public float actionCooldown = 20f;
        public float minMoveDistance = 4f;
        public float maxMoveDistance = 70f;
        public float combatMoveDistance = 10f;
        private float _lastActionTime;
        private bool _isSleeping = false;
        private bool _isSitting = false;
        private bool _isMoving = false;

        [Header("Attack Prefab")]
        public GameObject bearHit;

        private NavMeshAgent _agent;
        private Animator _animator;
        private Vector3 _spawnPosition;

        // attack and general bools
        public bool isNotActive = false;
        private bool _isActive = false;
        public bool isDead = false;
        private bool _isAttacking = false;
        private bool _isBuff = false;

        [Header("Audio")]

        // this audio thing should work some work to be done on the range etc but very scary when bear running and screaming
        public AudioSource audiSource;
        public AudioClip audiClip;


        private void Start()
        {
            isNotActive = true;
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Animator not found on " + gameObject.name);
                return;
            }
            _spawnPosition = transform.position;
            Health = 50;
            _lastActionTime = Time.time;

            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
                Debug.Log("Player not assigned to " + gameObject.name + ", finding via tag");
            }
        }

        private void Update()
        {
            
            Debug.Log(gameObject.transform.position + "" + _agent.destination);
            if (isDead)
            {
                return;
            }
            // ich habe keine Ahnung, warum hier zweimal isactive steht, habe ich heute um 3:00 Uhr gemacht lol wenn ich eins wegmache gehts nicht mehr
            if (isNotActive)
            {
                if (!_isActive)
                {
                    SetAnimation(Idle);
                    isDead = false;
                    Health = 50;
                    _lastActionTime = Time.time;
                    Debug.Log("Bear set to active");
                    _isActive = true;
                    SetAnimation(Idle);
                    return;
                }
            }
            if (!isNotActive && _isActive)
            {
                Debug.Log("Bear set to inactive");
                SetAnimation(Idle);
                isDead = false;
                Health = 50;
                _lastActionTime = Time.time;
                return;
            }

            // ATTACK
            // if player is in range for activation
            if (IsInRange(player, activationRange))
            {
                _agent.speed = 7f;
                if (_isAttacking)
                {
                    _agent.SetDestination(player.transform.position);
                    if (Time.time - _lastActionTime > attackCooldown && IsInRange(player, attackRange))// player in attack range: in hit distance
                    {
                        _lastActionTime = Time.time;
                        StartCoroutine(ExecuteRandomAttack());
                    }
                }
                else
                {
                    if (!_isBuff)
                    {
                        _isMoving = false;
                        StartCoroutine(Buff());
                    }

                }
            }
            // NORMAL BEHAVIOUR 
            // if player not in range
            // also stops the attack mode
            // randomly chooses normal behaviour after a cooldown 
            else
            {
                _agent.speed = 2f;
                _isAttacking = false;

                if (Time.time - _lastActionTime > actionCooldown)
                {
                    _lastActionTime = Time.time;
                    StartCoroutine(StartRandomBehaviour());
                }

                // if the bear is in very close distance to the target of the agent destination, stop, Idle animation
                if (_isMoving)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, _agent.destination);
                    if (distanceToTarget < 0.5)
                    {
                        Debug.Log("Stop Move");
                        SetAnimation(Idle);
                        _isMoving = false;
                    }
                }
            }
        }

        private void SetAnimation(int animationName)
        {
            if (_currentAnimation == animationName) return;
            _animator.Play(animationName);
            _currentAnimation = animationName;
        }

        // move to random location on navmesh
        private void MoveToRandomLocation()
        {
            Debug.Log("Start move");
            _isMoving = true;
            _agent.SetDestination(RandomNavmeshLocation(maxMoveDistance, minMoveDistance));
            Debug.Log(gameObject.transform.position + "" + _agent.destination);
            SetAnimation(WalkForward);
        }

        // this method is for when the player group tells me what thing hits the bear when he should take damage
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Weapon"))
            {
                TakeDamage(2);
            }
        }

        // here the bear turns to player and screams at him, after that he attacks
        private IEnumerator Buff()
        {
            _isBuff = true;
            gameObject.transform.LookAt(player.transform.position);
            SetAnimation(Roar);
            audiSource.PlayOneShot(audiClip);
            yield return new WaitForSeconds(2);
            gameObject.transform.LookAt(player.transform.position);
            SetAnimation(Roar);
            yield return new WaitForSeconds(2);
            SetAnimation(RunForward);
            _isAttacking = true;
            _isBuff = false;
        }

        // choses a attack randomly, instantiates a prefab that is for the player to check if he is being hit
        private IEnumerator ExecuteRandomAttack()
        {
            int attackType = UnityEngine.Random.Range(0, 4);
            switch (attackType)
            {
                case 0:
                    SetAnimation(Attack1);
                    Instantiate(bearHit, transform.position, transform.rotation);
                    break;
                case 1:
                    SetAnimation(Attack2);
                    Instantiate(bearHit, transform.position, transform.rotation);
                    break;
                case 2:
                    SetAnimation(Attack3);
                    Instantiate(bearHit, transform.position, transform.rotation);
                    break;
                case 3:
                    SetAnimation(Attack4);
                    Instantiate(bearHit, transform.position, transform.rotation);
                    break;
            }
            yield return null;
        }

        // The following randomly chooses a normal, non attack behavioure to execute:
        private IEnumerator StartRandomBehaviour()
        {
            int attackType = UnityEngine.Random.Range(0, 6);
            switch (attackType)
            {
                case 0:// Sit down
                    Debug.Log("Sit");
                    _isSitting = true;
                    _isSleeping = false;
                    _isMoving = false;
                    _agent.isStopped = true;
                    SetAnimation(Sit);
                    break;
                case 1:// Stand up
                    Debug.Log("Stand");
                    _isSitting = false;
                    _isSleeping = false;
                    _isMoving = false;
                    _agent.isStopped = true;
                    SetAnimation(Idle);
                    break;
                case 2:// sleep
                    Debug.Log("Sleep");
                    _isSitting = false;
                    _isSleeping = true;
                    _isMoving = false;
                    _agent.isStopped = true;
                    SetAnimation(Sleep_A);
                    break;
                case 3:// Walk to random location
                    Debug.Log("Walk");
                    _isSitting = false;
                    _isSleeping = false;
                    _isMoving = true;
                    MoveToRandomLocation();
                    break;
                case 4:// Walk to random location
                    Debug.Log("Walk");
                    _isSitting = false;
                    _isSleeping = false;
                    _isMoving = true;
                    MoveToRandomLocation();
                    break;
                case 5:// Walk to random location
                    Debug.Log("Walk");
                    _isSitting = false;
                    _isSleeping = false;
                    _isMoving = true;
                    MoveToRandomLocation();
                    break;
            }
            yield return null;
        }

        public bool IsHeDead()
        {
            if (Health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Die()
        {
            if (isDead) return;
            isDead = true;
            SetAnimation(Death);
            _agent.isStopped = true;
        }

        // no real usage for this yet, because I'm waiting for the player group to tell me their collider
        public int TakeDamage(int amount)
        {
            Health -= amount;
            if (IsHeDead())
            {
                Die();
            }
            return Health;
        }
    }
}
