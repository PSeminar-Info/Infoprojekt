using UnityEngine;
using UnityEngine.UI;

namespace Entities.Npc.Friendly.Villager
{
    public class TriggerText : MonoBehaviour
    {
        public Text text;
        public Text info;
        private int _countTillNextPart;

        public string[] phrasesArray = {
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
        private string _randomPhrase;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                text.gameObject.SetActive(true);
                var random = new System.Random();
                var randomIndex = random.Next(phrasesArray.Length);
                _randomPhrase = phrasesArray[randomIndex];
                _countTillNextPart++;
                text.text = _randomPhrase;
            };
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) text.gameObject.SetActive(false);
        }

        public void Update()
        {
            if (_countTillNextPart > 3)
            {
                info.text = "Begib dich in den Wald. Du kannst dich am Starthaus teleportieren";
            }
        }
    }
}
