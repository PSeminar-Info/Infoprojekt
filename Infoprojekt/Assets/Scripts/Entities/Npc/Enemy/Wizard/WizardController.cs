using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc.Enemy.Wizard
{
    public class WizardController : Npc
    // the animator needs to be in the child so it doesn't teleport to 0 0 0
    
    {
        [Tooltip("Cooldown between moving action (when not in combat)")]
        public float moveCooldown = 15f;

        public float portalCooldown = 10f;

        public float attackCooldown = 5f;
        public float attackRange = 20f;

        public float maxTeleportDistance = 15f;
        public float minTeleportDistance = 5f;

        public float minMoveDistance = 5f;
        public float maxMoveDistance = 10f;

        public GameObject player;
        public GameObject portal;
        public GameObject bigAttack;
        public GameObject smallAttack;
        public GameObject homingAttack;

        private NavMeshAgent _agent;
        private Vector3 _spawnPosition;
        private Animator _animator;

        private float _lastActionTime;


        private void Start()
        {
            _lastActionTime = Time.time;
            _agent = GetComponent<NavMeshAgent>();
            _spawnPosition = transform.position;
            _animator = transform.GetComponentInChildren<WizardAnimationScript>().animator;
            Invoke(nameof(LightAttack), 5f);
        }

        private void Update()
        {
            if (Time.time - _lastActionTime > portalCooldown && transform.name == "Wizard")
            {
                _lastActionTime = Time.time;
                StartCoroutine(nameof(TeleportToRandomLocation));
            }
        }

        // wizard should teleport away from player if they get too close, second portal should also face the player
        private IEnumerator TeleportToRandomLocation()
        {
            // wizard will need to face the player so the portal is facing the right direction
            var destination = RandomNavmeshLocation(maxTeleportDistance, minTeleportDistance);
            var wizard = transform;
            var rotation = wizard.rotation;

            // using 5 for simplicity, needs to be changed if the portal duration changes
            Destroy(Instantiate(portal, destination, rotation), 5);
            yield return new WaitForSeconds(0.25f);
            Destroy(Instantiate(portal, wizard.position, rotation), 5);
            yield return new WaitForSeconds(1f);
            _agent.Warp(destination);
        }

        private void MoveToRandomLocation()
        {
            _agent.SetDestination(RandomNavmeshLocation(maxMoveDistance, minMoveDistance));
        }

        // 3 projectiles in player direction
        private void LightAttack()
        {
            var wiz = transform;
            Instantiate(smallAttack, wiz.position, wiz.rotation);
        }

        // 5 projectiles in all directions
        private void HeavyAttack()
        {
            var wiz = transform;
            Instantiate(bigAttack, wiz.position, wiz.rotation);
        }

        // one projectile
        private void HomingAttack()
        {
            var wiz = transform;
            Instantiate(homingAttack, wiz.position, wiz.rotation);
        }
    }
}