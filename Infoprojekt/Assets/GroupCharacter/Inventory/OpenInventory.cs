using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject panel; // Referenz auf das zu öffnende Panel
    public GameObject Inventoryman;
    InventoryManager invman;
    public Transform ItemContent;

    void Start()
    {
        invman = Inventoryman.GetComponent<InventoryManager>();
    }
    void Update()
    {
        // Überprüfe, ob die Taste "E" gedrückt wurde
        if (Input.GetKeyDown(KeyCode.I))
        {

            // Öffne das Panel, falls es nicht bereits aktiv ist
            if (!panel.activeSelf)
            {
                panel.SetActive(true);
                invman.ListItems();

            }
            else
            {
                panel.SetActive(false);
                foreach (Transform item in ItemContent)
                {
                    Destroy(item.gameObject);
                }
            }
        }
        
    }
}
