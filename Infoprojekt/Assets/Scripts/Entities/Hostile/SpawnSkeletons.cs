using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Entities.Hostile
{
    public class SpawnSkeletons : MonoBehaviour
    {
        public Text info;
        [FormerlySerializedAs("SkeletonArmy")] public GameObject skeletonArmy;

        private void OnTriggerEnter(Collider other)
        {
            skeletonArmy.SetActive(true);
            if (other.CompareTag("Player")) info.text = "Bleibe auf dem Weg und halte dich fern von den Skeletten ... vielleicht kannst du dich ja irgendwo verstecken";
        }
    }
}
