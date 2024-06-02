using UnityEngine;
using UnityEngine.Serialization;

namespace GroupCharacter.Inventory
{
    public class OpenInventory : MonoBehaviour
    {
        public GameObject panel; // Referenz auf das zu öffnende Panel
        [FormerlySerializedAs("Inventoryman")] public GameObject inventoryman;
        [FormerlySerializedAs("ItemContent")] public Transform itemContent;
        private InventoryManager _invman;

        private void Start()
        {
            _invman = inventoryman.GetComponent<InventoryManager>();
        }

        private void Update()
        {
            // Überprüfe, ob die Taste "E" gedrückt wurde
            if (Input.GetKeyDown(KeyCode.I))
            {
                // Öffne das Panel, falls es nicht bereits aktiv ist
                if (!panel.activeSelf)
                {
                    panel.SetActive(true);
                    _invman.ListItems();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                }
                else
                {
                    panel.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    foreach (Transform item in itemContent) Destroy(item.gameObject);
                }
            }
        }
    }
}