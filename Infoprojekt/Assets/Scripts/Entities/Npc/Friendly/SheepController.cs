using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc.Friendly
{
    public class SheepController : Npc
    {
        // will use animations in animation import, not in animations folder. 
        public float maxTargetDistance;
        public float minTargetDistance;

        public float cooldown;

        // Movement
        private NavMeshAgent _agent;
        private Animator _animator;

        // Animations: walk_forward, (walk_backwards), run_forward, turn_90_L, turn_90_R, trot_forward, sit_to_stand, stand_to_sit, idle
        private string _currentAnimation;
        private Vector3 _destination;
        private float _lastActionTime;


        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            InvokeRepeating(nameof(RandomAction), 0, cooldown);
        }

        private void Update()
        {
            if (_agent.destination == transform.position && _currentAnimation != "stand_to_sit" &&
                _currentAnimation != "sit_to_stand") SetAnimation("idle");
        }

        private static void OnDeath()
        {
            // TODO: death animation, drop items
        }

        private void SetAnimation(string animationName)
        {
            if (_currentAnimation == animationName) return;
            _animator.Play(animationName);
            _currentAnimation = animationName;
            Debug.Log("Set animation to: " + animationName);
        }

        // rotate to face the target location, then walk to it
        // needs to be changed in order to use different animations (
        private void WalkToRandomLocation(string animationName = "walk_forward")
        {
            _destination = RandomNavmeshLocation(maxTargetDistance, minTargetDistance);
            var targetRotation = Quaternion.LookRotation(_destination - transform.position);

            if (targetRotation.eulerAngles.y is > 45 and < 180 && _currentAnimation != "turn_90_R")
                SetAnimation("turn_90_R");
            else if (targetRotation.eulerAngles.y is > 180 and < 315 && _currentAnimation != "turn_90_L")
                SetAnimation("turn_90_L");
            Invoke(nameof(StartWalking), 1);
        }

        private void StartWalking(string animationName = "walk_forward")
        {
            // remove when using WalkToRandomLocation()!!
            _destination = RandomNavmeshLocation(maxTargetDistance, minTargetDistance);

            _agent.SetDestination(_destination);
            SetAnimation(animationName);
        }

        private void RandomAction()
        {
            if (_currentAnimation == "stand_to_sit")
            {
                SetAnimation("sit_to_stand");
                return;
            }

            var random = Random.Range(0, 5);
            switch (random)
            {
                case 0:
                    SetAnimation("stand_to_sit");
                    return;
                case 1:
                    StartWalking();
                    return;
                case 2:
                    StartWalking("trot_forward");
                    return;
                case 3:
                    StartWalking("run_forward");
                    return;
                case 4:
                    SetAnimation("eating");
                    return;
            }
        }
    }
}