using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc.Enemy.Spider
{
    public class SpiderController : Npc
    {
        // if checks become too laggy, use a coroutine to check every 0.5 seconds or so
        // coroutine should only be started when the player is in a certain range
        
        public float maxTargetDistance = 3f;
        public float minTargetDistance = 1f;

        [Tooltip("Minimum cooldown when not in combat")]
        public float minimumCooldown = 1.5f;

        [Tooltip("Maximum cooldown when in combat")]
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
            if (IsInPlayerRange(player, attackRange)) _agent.SetDestination(player.transform.position);
            if (Health <= 0) StartCoroutine(nameof(OnDeath));
        }

        private void MoveToRandomLocationIfNotAttacking()
        {
            if (!IsInPlayerRange(player, attackRange))
                _agent.SetDestination(RandomNavmeshLocation(maxTargetDistance, minTargetDistance));
        }

        private IEnumerable OnDeath()
        {
            yield return new WaitForSeconds(deathTime);
            Destroy(gameObject);
        }
    }
}