using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class SpawnSkelletons : MonoBehaviour
{
    public Text info;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) info.text = "Ja also hier sollten jetzt Seppis Skelette sein. Hab sie aber bis 1.6 (14:00) nicht bekommen. Gehe zur Kapelle";
    }
}
