using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;

    void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("jghjugb");
            PickUp();
        }
    }
}
