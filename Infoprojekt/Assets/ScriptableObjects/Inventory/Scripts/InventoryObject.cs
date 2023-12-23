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