using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Entities.Npc.Friendly.Villager
{
    public class TriggerText : MonoBehaviour
    {
        public Text text;
        public Text info;

        public string[] phrasesArray =
        {
            "Ich habe keine Zeit für deine Anliegen.",
            "Geh weg! Ich will nicht mit dir reden.",
            "Lass mich in Ruhe. Ich habe wichtigere Dinge zu tun.",
            "Deine Anwesenheit ist hier nicht erwünscht.",
            "Ich möchte keine Geschäfte mit dir machen.",
            "Du störst mich. Verschwinde!",
            "Ich habe keine Lust, mich mit dir abzugeben.",
            "Ich möchte allein sein. Geh bitte.",
            "Verschwinde, bevor ich die Wachen rufe.",
            "Ich will nichts mit Fremden zu tun haben."
        };

        private int _countTillNextPart;
        private string _randomPhrase;

        public void Update()
        {
            if (_countTillNextPart > 3) info.text = "Begib dich in den Wald und laufe alle Wege ab um den Bären zu finden. Du kannst dich am Starthaus teleportieren";
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                text.gameObject.SetActive(true);
                var random = new Random();
                var randomIndex = random.Next(phrasesArray.Length);
                _randomPhrase = phrasesArray[randomIndex];
                _countTillNextPart++;
                text.text = _randomPhrase;
            }

            ;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) text.gameObject.SetActive(false);
        }
    }
}