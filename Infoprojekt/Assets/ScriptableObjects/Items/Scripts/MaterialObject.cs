using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    [CreateAssetMenu(fileName = "New Material Object", menuName = "Inventory/Items/Material")]
    public class MaterialObject : ItemObject
    {
        public void Awake()
        {
            type = ItemType.Material;
        }
    }
}