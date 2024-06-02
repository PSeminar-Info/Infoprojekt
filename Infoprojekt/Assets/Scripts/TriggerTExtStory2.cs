using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerTExtStory2 : MonoBehaviour
{
    public Text info;
    private void OnTriggerEnter(Collider other)
    {
        info.text = "Gehe zur Burg und kämpfe gegen Malakar";
    }
}
