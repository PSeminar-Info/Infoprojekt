using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject panel; // Referenz auf das zu �ffnende Panel
    public GameObject Inventoryman;
    InventoryManager invman;
    void Start()
    {
        invman = Inventoryman.GetComponent<InventoryManager>();
    }
    void Update()
    {
        // �berpr�fe, ob die Taste "E" gedr�ckt wurde
        if (Input.GetKeyDown(KeyCode.E))
        {

            // �ffne das Panel, falls es nicht bereits aktiv ist
            if (!panel.activeSelf)
            {
                panel.SetActive(true);
                invman.ListItems();

            }
            else
            {
                panel.SetActive(false);
            }
        }
        
    }
}
