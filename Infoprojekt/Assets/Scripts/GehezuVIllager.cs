using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GehezuVIllager : MonoBehaviour
{
    public Text info;

    private void OnTriggerEnter(Collider other)
    {
        info.text = "Suche den Villager am Haus auf";
    }
}
