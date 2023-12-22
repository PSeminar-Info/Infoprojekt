using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    Item item;
    public void Removeitem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }
    public void AddItem(Item newItem)
    {
        item = newItem;

    }
}
