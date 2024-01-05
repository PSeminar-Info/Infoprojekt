using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

namespace ScriptableObjects.Inventory.Scripts
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public List<InventorySlot> items = new();

        public void AddItem(ItemObject item, int amount)
        {
            foreach (var slot in items.Where(slot => slot.item == item))
            {
                slot.AddAmount(amount);
                return;
            }

            items.Add(new InventorySlot(item, amount));
        }

        public void dropAllItems(Vector3 position)
        {
            foreach (var slot in items)
                for (var i = 0; i < slot.amount; i++)
                    // TODO: items need trigger colliders etc. to they can be interacted with
                    Instantiate(slot.item.prefab, position, Quaternion.identity);
            items.Clear();
        }
    }

    [Serializable]
    public class InventorySlot
    {
        public ItemObject item;
        public int amount;

        public InventorySlot(ItemObject item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }
    }
}