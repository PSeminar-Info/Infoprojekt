using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc.Enemy.Bear
{
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

        [Header("Movement")]
        public float moveCooldown = 12f;
        public float minMoveDistance = 4f;
        public float maxMoveDistance = 70f;
        public float combatMoveDistance = 10f;
        private float _lastMoveTime;
        private float _lastSitTime;
        private float _lastSleepTime;
        public float sittingCooldown = 15f;
        public float sleepCooldown = 20f;
        public float awakeCooldown = 13f;

        [Header("Attack Prefab")]
        public GameObject bearHit;

        private NavMeshAgent _agent;
        private Animator _animator;

        private float _lastActionTime;
        private Vector3 _spawnPosition;

        private bool _isSleeping = false;
        private bool _isSitting = false;
        private bool _isDead = false;
        private bool _isAttacking = false;
        private bool _isMoving = false;
        private bool _isBuff = false;
        private bool _isRunning = false;

        [Header("Audio")]

        public AudioSource audiSource;
        public AudioClip audiClip;


        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Animator not found on " + gameObject.name);
                return;
            }
            _spawnPosition = transform.position;
            Health = 50;
            _lastMoveTime = Time.time;
            _lastSitTime = Time.time;
            _lastSleepTime = Time.time;

            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
                Debug.Log("Player not assigned to " + gameObject.name + ", finding via tag");
            }
        }

        private void Update()
        {
            // if player in range: Attack stuff
            if (IsInRange(player, activationRange))
            {
                Debug.Log("In range");
                _agent.speed = 7f;
                if (_isAttacking)
                {
                    Debug.Log(_isRunning);
                    _agent.SetDestination(player.transform.position);
                    if (Time.time - _lastActionTime > attackCooldown && IsInRange(player, attackRange))
                    {
                        StartCoroutine(ExecuteRandomAttack());
                        _lastActionTime = Time.time;
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
            // if player !in range: stop attack stuff, start normal
            else
            {
                Debug.Log("else");
                _agent.speed = 2f;
                _isAttacking = false;
                if (Time.time - _lastMoveTime > moveCooldown)
                {
                    Debug.Log("Move");
                    _lastMoveTime = Time.time;
                    MoveToRandomLocation();
                }
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
                if (Time.time - _lastMoveTime < moveCooldown && !_isMoving)
                {
                    Debug.Log("Test");
                    if (Time.time - _lastSitTime > sittingCooldown)
                    {
                        if (_isSitting)
                        {
                            StandUp();
                        }
                        else
                        {
                            SitDown();
                        }
                    }
                    else if (Time.time - _lastSleepTime > sleepCooldown)
                    {
                        if (_isSleeping)
                        {

                            Awake();
                        }
                        else
                        {
                            Sleep();
                        }
                    }
                    else
                    {
                        SetAnimation(Idle);
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

        private void MoveToRandomLocation()
        {
            _isMoving = true;
            _agent.SetDestination(RandomNavmeshLocation(maxMoveDistance, minMoveDistance));
            SetAnimation(WalkForward);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Weapon"))
            {
                Health -= 1;
            }
        }

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

        private void SitDown()
        {
            _lastSitTime = Time.time;
            _isSitting = true;
            SetAnimation(Sit);
        }

        private void StandUp()
        {
            _lastSitTime = Time.time;
            _isSitting = false;
            SetAnimation(Stand);
        }

        private void Sleep()
        {
            _lastSleepTime = Time.time;
            _isSleeping = true;
            SetAnimation(Sleep_A);
        }

        private void Awake()
        {
            _lastSleepTime = Time.time;
            _isSleeping = false;
            SetAnimation(Idle);
        }

        public void Die()
        {
            if (_isDead) return;
            _isDead = true;
            SetAnimation(Death);
            _agent.isStopped = true;
        }
    }
}
