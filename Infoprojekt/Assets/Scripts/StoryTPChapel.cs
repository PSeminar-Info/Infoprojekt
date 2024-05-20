using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject LazyLake;
    public GameObject StartMap;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindWithTag("Player").SetActive(false);
            LazyLake.SetActive(true);
            GameObject.FindWithTag("Player").transform.position = new Vector3(-154, 15, -19);
            StartMap.SetActive(true);
            GameObject.FindWithTag("Player").SetActive(true);
        }
    }
}
