using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerText : MonoBehaviour
{
    public Text text;

    public string[] stringArray = { "Ich habe keine Zeit für deine Anliegen.", "Geh weg! Ich will nicht mit dir reden.", "Lass mich in Ruhe. Ich habe wichtigere Dinge zu tun.", "Deine Anwesenheit ist hier nicht erwünscht.", "Ich möchte keine Geschäfte mit dir machen.", "Du störst mich. Verschwinde!", "Ich habe keine Lust, mich mit dir abzugeben.", "Ich möchte allein sein. Geh bitte.", "Verschwinde, bevor ich die Wachen rufe.", "Ich will nichts mit Fremden zu tun haben." };
    private string randomString;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
            System.Random random = new System.Random();

            // Wähle einen zufälligen Index aus dem Array
            int randomIndex = random.Next(stringArray.Length);

            // Hole den String an diesem zufälligen Index
            randomString = stringArray[randomIndex];

            text.text = randomString;
        };
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) text.gameObject.SetActive(false);
    }

}
