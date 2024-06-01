using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillagerFishersLake : MonoBehaviour
{
    public Text info;

    public void OnTriggerEnter(Collider other)
    {
        info.text = "Gehe zur Burg";
    }
}
