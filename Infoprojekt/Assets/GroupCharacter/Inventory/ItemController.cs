using UnityEngine;
using UnityEngine.Serialization;

namespace GroupCharacter.Inventory
{
    public class ItemController : MonoBehaviour
    {
        [FormerlySerializedAs("Item")] public Item item;
    }
}