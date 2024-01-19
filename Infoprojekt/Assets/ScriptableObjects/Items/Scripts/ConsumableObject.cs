using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    [CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory/Items/Consumable")]
    public class ConsumableObject : ItemObject
    {
        public int healAmount;

        public void Awake()
        {
            type = ItemType.Consumable;
        }
    }
}