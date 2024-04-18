using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc.Enemy.Skeleton
{
    public class SkeletonController : Npc
    {
        private NavMeshAgent _nav;
        private readonly float maxmove = 30f;
        private readonly float minmove = 10f;
        private string state = "Idle";

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            switch (state)
            {
                case "Idle":
                    Patrolling();
                    break;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) state = "Pursuit";
        }

        private void Patrolling()
        {
            _nav.SetDestination(RandomNavmeshLocation(minmove, maxmove));
        }
    }
}