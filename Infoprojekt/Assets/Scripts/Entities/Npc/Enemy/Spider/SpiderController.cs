using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc.Enemy.Spider
{
    public class SpiderController : Npc
    {
        public float maxTargetDistance = 3f;
        public float minTargetDistance = 1f;
        public float minimumCooldown = 1.5f;
        public float maximumCooldown = 3f;
        public float attackRange = 4f;
        public GameObject player;

        public float deathTime = 1f;
        private NavMeshAgent _agent;
        private Vector3 _destination;
        private float _lastActionTime;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            InvokeRepeating(nameof(MoveToRandomLocationIfNotAttacking), 0,
                Random.Range(minimumCooldown, maximumCooldown));
        }

        private void FixedUpdate()
        {
            if (IsInPlayerRange()) _agent.SetDestination(player.transform.position);
        }

        private void MoveToRandomLocationIfNotAttacking()
        {
            if (!IsInPlayerRange())
                _agent.SetDestination(RandomNavmeshLocation(maxTargetDistance, minTargetDistance));
        }

        private bool IsInPlayerRange()
        {
            // check if player tag is in a sphere of radius attackRange around the spider
            var colliders = Physics.OverlapSphere(transform.position, attackRange);
            return colliders.Any(col => col.CompareTag("Player"));
        }
    }
}