using Unity.VisualScripting;
using UnityEngine;

namespace Entities.Npc.Friendly.Villager
{
    public class VillagerController : MonoBehaviour
    {
        public float rotationSpeed = 5f;

        public GameObject player;

        private void Start()
        {
            if (player.IsUnityNull())
            {
                Debug.Log($"Player not assigned to {gameObject.name}, finding via tag.");
                player = GameObject.FindWithTag("Player");
            }
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 10)
            {
                var lookPos = player.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}