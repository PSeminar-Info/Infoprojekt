using UnityEngine;
using UnityEngine.UI;

namespace Entities.Npc.Friendly.Villager
{
    

    public class VillagerFishersLake : MonoBehaviour
    {
        public Text info;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))  {
            info.text = "Gehe zur Ruine in der Tundra";
            }
        }
        
    }
}