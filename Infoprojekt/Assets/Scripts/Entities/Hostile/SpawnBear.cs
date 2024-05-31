using Entities.Npc.Enemy.Bear;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBear : MonoBehaviour
{
    public GameObject Bear;
    BearController BearController;
    public Text info;

    public void Start()
    {
        BearController = Bear.GetComponent<BearController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Bear.SetActive(true);
    }

    public void Update()
    {
        if (BearController.isDead)
        {
            print("geh zum friedhof");
            info.text = "Suche den Friedhof";
        }
    }
}
