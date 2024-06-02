using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject LazyLake;
    public GameObject StartMap;
    public GameObject Player;
    public Text info;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.SetActive(false);
            LazyLake.SetActive(true);
            Player.transform.position = new Vector3(-154, 15, -19);
            StartMap.SetActive(false);
            Player.SetActive(true);
            info.text = "Gehe zum Villager am Haus";
        }
    }
}
