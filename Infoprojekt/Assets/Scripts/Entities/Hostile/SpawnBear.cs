using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBear : MonoBehaviour
{
    public GameObject Bear;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Bear.SetActive(true);
    }
}
