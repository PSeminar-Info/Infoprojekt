using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [Tooltip("Movement type the horse will use when walking to a specific location.")]
        public Options movementType;

        public List<Transform> destinations;

        public enum Options
        {
            Walk,
            Trot,
            Run,
        }

        public float minimumCooldown = 4f;
        public float maximumCooldown = 6f;

        public float maxTargetDistance = 7.5f;
        public float minTargetDistance = 3f;

        [Header("Rider")] public GameObject rider;
        public Transform ridingPosition;

        private NavMeshAgent _agent;
        private Animator _animator;

        private int _currentAnimation;

        private Vector3 _destination;
        private bool _movingToDestination;

        // _maxDistance and _minDistance are used to increase distance when running/trotting, walking uses default values
        private float _maxDistance;
        private float _minDistance;

        private bool _hasDestination;
        private int _sampledPositions;

        private void Start()
        {
            _minDistance = minTargetDistance;
            _maxDistance = maxTargetDistance;
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            foreach (var dest in destinations.ToList())
            {
                if (NavMesh.SamplePosition(dest.position, out var hit, 4, 1))
                {
                    dest.position = hit.position;
                    _sampledPositions++;
                }
                else
                {
                    Debug.LogWarning($"Destination {dest.name} is not close to the navmesh. Removing it.");
                    destinations.Remove(dest);
                }
            }

            if (destinations[0])
            {
                _agent.destination = destinations[0].position;
                PlayCorrectAnimation();
            }

            InvokeRepeating(nameof(RandomAction), 0, Random.Range(minimumCooldown, maximumCooldown));
        }

        private void Update()
        {
            if (rider)
            {
                rider.transform.position = ridingPosition.position;
            }

            if (_sampledPositions < destinations.Count)
            {
                foreach (var dest in destinations.ToList())
                {
                    if (NavMesh.SamplePosition(dest.position, out var hit, 4, 1))
                    {
                        dest.position = hit.position;
                        _sampledPositions++;
                    }
                    else
                    {
                        Debug.LogWarning($"Destination {dest.name} is not close to the navmesh. Removing it.");
                        destinations.Remove(dest);
                    }
                }
            }

            if (Vector3.Distance(_agent.destination, transform.position) < 0.4)
            {

                _hasDestination = false;
                if (destinations.Count > 0)
                {
                    destinations.RemoveAt(0);
                }
                else SetAnimation(Idle);

                _agent.ResetPath();
            }

            if (destinations.Count > 0 && !_hasDestination)
            {
                _agent.SetDestination(destinations[0].position);
                PlayCorrectAnimation();
                _hasDestination = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") == rider)
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
        }

        private void PlayCorrectAnimation()
        {
            switch (movementType)
            {
                case Options.Walk:
                    _animator.Play(WalkForward);
                    _currentAnimation = WalkForward;
                    break;
                case Options.Trot:
                    _animator.Play(TrotForward);
                    _currentAnimation = TrotForward;
                    break;
                case Options.Run:
                    _animator.Play(RunForward);
                    _currentAnimation = RunForward;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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

            switch (targetRotation.eulerAngles.y)
            {
                case > 45 and < 180:
                    SetAnimation(Turn90R);
                    break;
                case > 180 and < 315:
                    SetAnimation(Turn90L);
                    break;
            }

            StartCoroutine(StartWalkingAfterRotation(animationName));
        }

        private IEnumerator StartWalkingAfterRotation(int animationName)
        {
            yield return new WaitForSeconds(1f);

            _agent.SetDestination(_destination);
            SetAnimation(animationName);
        }

        private void RandomAction()
        {
            if (disableRandomMovement || destinations.Count > 0) return;
            var random = Random.Range(0, 2);
            switch (random)
            {
                case 0:
                    _minDistance = minTargetDistance;
                    _maxDistance = maxTargetDistance;
                    WalkToRandomLocation(WalkForward);
                    return;
                case 1:
                    SetAnimation(Eat);
                    return;
            }
        }
    }
}