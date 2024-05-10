using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Entities.Npc.Friendly.Horse
{
    public class Horse : Npc
    {
        private static readonly int Death = Animator.StringToHash("death");
        private static readonly int HitReaction = Animator.StringToHash("hit_reaction");
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int Eat = Animator.StringToHash("eat");
        private static readonly int WalkForward = Animator.StringToHash("walk_forward");
        private static readonly int RunForward = Animator.StringToHash("run_forward");
        private static readonly int TrotForward = Animator.StringToHash("trot_forward");
        private static readonly int Turn90L = Animator.StringToHash("turn_90_L");
        private static readonly int Turn90R = Animator.StringToHash("turn_90_R");

        public float deathTime = 2f;

        [Header("Movement")] public bool disableRandomMovement;

        public float minimumCooldown = 4f;
        public float maximumCooldown = 6f;
        
        public float maxTargetDistance = 7.5f;
        public float minTargetDistance = 3f;

        [Header("Rider")] public GameObject rider;
        public Transform ridingPosition;

        // Animation: using hash instead of string for performance, setA
        private NavMeshAgent _agent;
        private Animator _animator;

        private int _currentAnimation;

        private Vector3 _destination;

        // _maxDistance and _minDistance are used to increase distance when running/trotting, walking uses default values
        private float _maxDistance;
        private float _minDistance;

        private void Start()
        {
            _minDistance = minTargetDistance;
            _maxDistance = maxTargetDistance;
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            
            InvokeRepeating(nameof(RandomAction), 0, Random.Range(minimumCooldown, maximumCooldown));
        }

        private void Update()
        {
            if (Vector3.Distance(_agent.destination, transform.position) < 0.3 &&
                _currentAnimation != Turn90L && _currentAnimation != Turn90R)
                SetAnimation(Idle);
            if (rider)
            {
                rider.transform.position = ridingPosition.position;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") == rider)
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
        }

        private void SetAnimation(int animationName)
        {
            if (_currentAnimation == animationName) return;
            _animator.Play(animationName);
            _currentAnimation = animationName;
        }

        private void OnDeath()
        {
            StartCoroutine(DeathAnimation());
        }

        private void OnHit()
        {
            // hit_reaction has a separate layer in the animation controller so it can be played on top of other animations
            _animator.SetTrigger(HitReaction);
        }

        private IEnumerator DeathAnimation()
        {
            SetAnimation(Death);
            yield return new WaitForSeconds(deathTime);

            var position = transform.position;
            inventory.DropAllItems(new Vector3(position.x, position.y + 1f, position.z));
            Destroy(gameObject);
        }

        private void WalkToRandomLocation(int animationName)
        {
            _destination = RandomNavmeshLocation(_maxDistance, _minDistance);
            var targetRotation = Quaternion.LookRotation(_destination - transform.position);

            // doesn't work yet, but can't be bothered to fix it
            if (targetRotation.eulerAngles.y is > 45 and < 180)
                SetAnimation(Turn90R);
            else if (targetRotation.eulerAngles.y is > 180 and < 315)
                SetAnimation(Turn90L);

            StartCoroutine(StartWalkingAfterRotation(animationName));
        }

        private IEnumerator StartWalkingAfterRotation(int animationName)
        {
            yield return new WaitForSeconds(1f);

            _agent.SetDestination(_destination);
            // _agent.angularSpeed = 45f;
            SetAnimation(animationName);
        }

        private void RandomAction()
        {
            if (disableRandomMovement) return;
            var random = Random.Range(0, 3);
            // TODO: eating animation is sometimes cancelled by other animations
            switch (random)
            {
                case 0:
                    _minDistance = minTargetDistance;
                    _maxDistance = maxTargetDistance;
                    WalkToRandomLocation(WalkForward);
                    return;
                case 1:
                    _minDistance = minTargetDistance + 3;
                    _maxDistance = maxTargetDistance + 3;
                    WalkToRandomLocation(TrotForward);
                    return;
                case 2:
                    SetAnimation(Eat);
                    return;
            }
        }
    }
}