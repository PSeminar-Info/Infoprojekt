using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc
{
    public abstract class Npc : Entity
    {
        /// <summary>
        ///     get a random point on the navmesh within a radius of the current position
        /// </summary>
        /// <param name="radius">Maximum distance from object</param>
        /// <returns>Vector3 position</returns>
        protected Vector3 RandomNavmeshLocation(float radius)
        {
            var randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            var finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out var hit, radius, 1)) finalPosition = hit.position;

            return finalPosition;
        }

        /// <summary>
        ///     get a random point on the navmesh within a radius of the current position, but at least minDistance away
        /// </summary>
        /// <param name="radius">Maximum distance from object</param>
        /// <param name="minDistance">Minimum distance from object</param>
        /// <returns>Vector3 position</returns>
        protected Vector3 RandomNavmeshLocation(float radius, float minDistance)
        {
            var randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            var finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out var hit, radius, 1)) finalPosition = hit.position;
            if (Vector3.Distance(finalPosition, transform.position) < minDistance)
                return RandomNavmeshLocation(radius, minDistance);
            return finalPosition;
        }

        protected bool IsInPlayerRange(GameObject player, float radius)
        {
            // OverlapSphereNonAlloc is faster than OverlapSphere and doesn't generate garbage, but will miss collisions if the array is too small
            // will need to increase the amount if there are a lot of colliders and the player doesn't get detected properly
            var overlapResults = new Collider[100];
            Physics.OverlapSphereNonAlloc(transform.position, radius, overlapResults);
            return overlapResults.Any(col => col.CompareTag("Player"));
        }
    }
}