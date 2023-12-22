using UnityEngine;

namespace Entities
{
    public class Item : MonoBehaviour
    {
        public enum ID
        {
            Wool,
            Meat
        }

        // only need graphics for player inventory
        public string ItemName { get; private set; }
        public int ItemAmount { get; private set; }
        public ID ItemID { get; private set; }


        public static Item CreateItem(ID id, int amount)
        {
            var item = new GameObject("Item").AddComponent<Item>();
            item.ItemName = GetItemName(id);
            item.ItemAmount = amount;
            item.ItemID = id;
            return item;
        }

        public void SetAmount(int amount)
        {
            if (amount < 0) throw new UnityException("Item amount cannot be negative");
            ItemAmount = amount;
        }

        public static string GetItemName(ID id)
        {
            return id switch
            {
                ID.Wool => "Wool",
                ID.Meat => "Meat",
                _ => throw new UnityException("Item ID name not found")
            };
        }
    }
}