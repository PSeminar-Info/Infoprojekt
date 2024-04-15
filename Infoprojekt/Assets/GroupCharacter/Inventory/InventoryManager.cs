using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GroupCharacter.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        [FormerlySerializedAs("Items")] public List<Item> items = new();

        [FormerlySerializedAs("ItemContent")] public Transform itemContent;
        [FormerlySerializedAs("InventoryItem")] public GameObject inventoryItem;

        [FormerlySerializedAs("InventoryItems")] public InventoryItemController[] inventoryItems;

        private void Awake()
        {
            Instance = this;
        }

        public void Add(Item item)
        {
            items.Add(item);
        }

        public void Remove(Item item)
        {
            Debug.Log(item);
            items.Remove(item);
        }

        public void ListItems()
        {
            foreach (Transform item in itemContent) Destroy(item.gameObject);

            foreach (var item in items)
            {
                var obj = Instantiate(inventoryItem, itemContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;
            }

            SetInventoryItems();
        }

        public void SetInventoryItems()
        {
            inventoryItems = itemContent.GetComponentsInChildren<InventoryItemController>();

            for (var i = 0; i < items.Count; i++) inventoryItems[i].AddItem(items[i]);
        }
    }
}