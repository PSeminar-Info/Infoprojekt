using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject panel; // Referenz auf das zu �ffnende Panel
    public GameObject Inventoryman;
    InventoryManager invman;
    public Transform ItemContent;

    void Start()
    {
        invman = Inventoryman.GetComponent<InventoryManager>();
    }
    void Update()
    {
        // �berpr�fe, ob die Taste "E" gedr�ckt wurde
        if (Input.GetKeyDown(KeyCode.I))
        {

            // �ffne das Panel, falls es nicht bereits aktiv ist
            if (!panel.activeSelf)
            {
                panel.SetActive(true);
                invman.ListItems();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale=0;

            }
            else
            {
                panel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale=1;
                foreach (Transform item in ItemContent)
                {
                    Destroy(item.gameObject);
                }
            }
        }
        
    }
}
