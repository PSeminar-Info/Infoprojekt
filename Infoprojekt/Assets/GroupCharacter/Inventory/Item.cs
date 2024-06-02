using UnityEngine;
using UnityEngine.Serialization;

namespace GroupCharacter.Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
    public class Item : ScriptableObject
    {
        public int id;
        public string itemName;
        public int value;
        [FormerlySerializedAs("Icon")] public Sprite icon;
    }
}