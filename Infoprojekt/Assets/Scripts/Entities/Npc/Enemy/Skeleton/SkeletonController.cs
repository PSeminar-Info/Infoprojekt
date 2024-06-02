using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc.Enemy.Skeleton
{
    public class SkeletonController : Npc
    {
        private NavMeshAgent _nav;
        private const float MaxMove = 30f;
        private const float MinMove = 10f;
        private string _state = "Idle";

        // Update is called once per frame
        private void Update()
        {
            switch (_state)
            {
                case "Idle":
                    Patrolling();
                    break;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) _state = "Pursuit";
        }

        private void Patrolling()
        {
            _nav.SetDestination(RandomNavmeshLocation(MinMove, MaxMove));
        }
    }
}