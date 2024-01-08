using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc.Enemy.Wizard
{
    public class WizardController : Npc
    // the animator needs to be in the child so it doesn't teleport to 0 0 0
    // animator shouldn't need to be paused anymore in order to move 

    // many behavioural things need the player to work properly, will do those after merge
    {
        public float moveCooldown = 15f;
        public float portalCooldown = 10f;

        public float attackCooldown = 5f;
        public float attackRange = 10f;

        public float maxTeleportDistance = 15f;
        public float minTeleportDistance = 5f;

        public float minMoveDistance = 5f;
        public float maxMoveDistance = 10f;

        public GameObject player;
        public GameObject portal;
        public GameObject bigProjectile;
        public GameObject smallProjectile;

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
            var portal0 = Instantiate(portal, destination, rotation);
            yield return new WaitForSeconds(0.25f);
            var portal1 = Instantiate(portal, wizard.position, rotation);
            yield return new WaitForSeconds(1f);
            _agent.Warp(destination);
            // will need to change this if the portal duration changes
            yield return new WaitForSeconds(5f);
            Destroy(portal0);
            Destroy(portal1);
        }

        private void MoveToRandomLocation()
        {
            _agent.SetDestination(RandomNavmeshLocation(maxMoveDistance, minMoveDistance));
        }

        private void LightAttack()
        {
        }

        private void HeavyAttack()
        {
        }
    }
}