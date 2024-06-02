using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class SpawnSkelletons : MonoBehaviour
{
    public Text info;
    public GameObject SkeletonArmy;

    private void OnTriggerEnter(Collider other)
    {
        SkeletonArmy.SetActive(true);
        if (other.CompareTag("Player")) info.text = "Bleibe auf dem Weg und halte dich fern von den Skeletten ... vielleicht kannst du dich ja irgendwo verstecken";
    }
}
