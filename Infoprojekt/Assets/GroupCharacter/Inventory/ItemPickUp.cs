using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;
    public bool EPressed;
    private GameObject panel; // Das GameObject ist jetzt private

    private void Start()
    {
        // Suchen Sie das Panel in der Hierarchie und weisen Sie es der privaten Variable zu
        panel = GameObject.Find("PanelPickUp"); // Ändern Sie "NameDesPanels" entsprechend dem Namen Ihres Panels

        if (panel != null) panel.SetActive(false); // Deaktivieren Sie das Panel beim Start
    }

    private void Update()
    {
        if (EPressed)
        {
            OpenPanel();

            PickUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) ClosePanel();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) OpenPanel();
    }

    private void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
        ClosePanel();
    }

    public void OpenPanel()
    {
        if (panel != null) panel.SetActive(true); // Aktivieren Sie das Panel
    }

    public void ClosePanel()
    {
        if (panel != null) panel.SetActive(false); // Aktivieren Sie das Panel
    }
}