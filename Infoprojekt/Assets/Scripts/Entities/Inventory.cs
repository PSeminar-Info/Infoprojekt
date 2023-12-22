using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities
{
    public class Inventory
    {
        private readonly List<Item> _items = new();
        private int InventorySize { get; set; }

        public void AddItem(Item.ID id, int amount)
        {
            if (_items.Count >= InventorySize)
            {
                Debug.Log("Inventory Full, could not add Item");
                return;
            }

            _items.Add(Item.CreateItem(id, amount));
        }

        public void RemoveItem(Item.ID item)
        {
            _items.Any(i =>
            {
                if (i.ItemID == item) _items.Remove(i);
                return true;
            });
        }

        public bool HasItem(Item.ID item)
        {
            return _items.Any(i => i.ItemID == item);
        }

        public List<Item> GetItems()
        {
            return _items;
        }

        public void SetInventorySize(int size)
        {
            InventorySize = size;
            if (_items.Count <= size) return;

            _items.RemoveRange(size, _items.Count - size);
            Debug.Log("removed " + (_items.Count - size) + " items from inventory to fit new size");
        }
    }
}