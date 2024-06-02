using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillagerFishersLake : MonoBehaviour
{
    public Text info;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  info.text = "Gehe zur Ruine in der Tundra";
    }
}
