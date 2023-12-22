using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;
    private GameObject panel; // Das GameObject ist jetzt private
    private void Start()
    {
        // Suchen Sie das Panel in der Hierarchie und weisen Sie es der privaten Variable zu
        panel = GameObject.Find("PanelPickUp"); // Ändern Sie "NameDesPanels" entsprechend dem Namen Ihres Panels

        if (panel != null)
        {
            panel.SetActive(false); // Deaktivieren Sie das Panel beim Start
        }
    }
    void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            OpenPanel();
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUp();
                ClosePanel();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ClosePanel();
        }
    }
    public void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true); // Aktivieren Sie das Panel
        }
    }
    public void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Aktivieren Sie das Panel
        }
    }
}
