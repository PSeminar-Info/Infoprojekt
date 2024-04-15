using UnityEngine;

namespace GroupCharacter.Inventory
{
    public class InventoryItemController : MonoBehaviour
    {
        private Item _item;

        public void Removeitem()
        {
            InventoryManager.Instance.Remove(_item);
            Destroy(gameObject);
        }

        public void AddItem(Item newItem)
        {
            _item = newItem;
        }
    }
}