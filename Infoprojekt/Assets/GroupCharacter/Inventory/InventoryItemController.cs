using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    private Item item;

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