using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc.Friendly.Sheep
{
    public class SheepController : Npc
    {
        private static readonly int Death = Animator.StringToHash("death");
        private static readonly int HitReaction = Animator.StringToHash("hit_reaction");
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int Eat = Animator.StringToHash("eat");
        private static readonly int SitToStand = Animator.StringToHash("sit_to_stand");
        private static readonly int StandToSit = Animator.StringToHash("stand_to_sit");
        private static readonly int WalkForward = Animator.StringToHash("walk_forward");
        private static readonly int RunForward = Animator.StringToHash("run_forward");
        private static readonly int TrotForward = Animator.StringToHash("trot_forward");
        private static readonly int Turn90L = Animator.StringToHash("turn_90_L");
        private static readonly int Turn90R = Animator.StringToHash("turn_90_R");

        public float deathTime = 2f;

        public float minimumCooldown = 4f;
        public float maximumCooldown = 6f;

        // Movement
        public float maxTargetDistance = 7.5f;
        public float minTargetDistance = 3f;

        // Animation: using hash instead of string for performance, setA
        private NavMeshAgent _agent;
        private Animator _animator;

        private int _currentAnimation;

        private Vector3 _destination;

        // _maxDistance and _minDistance are used to increase distance when running/trotting, walking uses default values
        private float _maxDistance;
        private float _minDistance;
        private float _startTime;


        private void Start()
        {
            _minDistance = minTargetDistance;
            _maxDistance = maxTargetDistance;

            _startTime = Time.time;

            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            InvokeRepeating(nameof(RandomAction), 0, Random.Range(minimumCooldown, maximumCooldown));
        }

        private void Update()
        {
            if (_agent.destination == transform.position && _currentAnimation != StandToSit &&
                _currentAnimation != SitToStand)
                SetAnimation(Idle);

            if (Time.time - _startTime > 15f)
            {
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
            // hit_reaction has a separate layer in the animation controller since it's so it can be played on top of other animations
            _animator.SetTrigger(HitReaction);
        }

        private IEnumerator DeathAnimation()
        {
            SetAnimation(Death);
            yield return new WaitForSeconds(deathTime);

            var position = transform.position;
            inventory.dropAllItems(new Vector3(position.x, position.y + 1f, position.z));
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
            if (_currentAnimation == StandToSit)
            {
                SetAnimation(SitToStand);
                return;
            }

            var random = Random.Range(0, 5);
            // TODO: eating animation is sometimes cancelled by other animations
            switch (random)
            {
                case 0:
                    SetAnimation(StandToSit);
                    return;
                case 1:
                    _minDistance = minTargetDistance;
                    _maxDistance = maxTargetDistance;
                    WalkToRandomLocation(WalkForward);
                    return;
                case 2:
                    _minDistance = minTargetDistance + 3;
                    _maxDistance = maxTargetDistance + 3;
                    WalkToRandomLocation(TrotForward);
                    return;
                case 3:
                    _minDistance = minTargetDistance + 5;
                    _maxDistance = maxTargetDistance + 5;
                    WalkToRandomLocation(RunForward);
                    return;
                case 4:
                    SetAnimation(Eat);
                    return;
            }
        }
    }
}