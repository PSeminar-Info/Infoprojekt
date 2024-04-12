using UnityEngine;
using UnityEngine.Serialization;

namespace GroupCharacter.Inventory
{
    public class ItemPickUp : MonoBehaviour
    {
        [FormerlySerializedAs("Item")] public Item item;
        [FormerlySerializedAs("EPressed")] public bool ePressed;
        private GameObject _panel; // Das GameObject ist jetzt private

        private void Start()
        {
            // Suchen Sie das Panel in der Hierarchie und weisen Sie es der privaten Variable zu
            _panel = GameObject.Find("PanelPickUp"); // Ändern Sie "NameDesPanels" entsprechend dem Namen Ihres Panels

            if (_panel != null) _panel.SetActive(false); // Deaktivieren Sie das Panel beim Start
        }

        private void Update()
        {
            if (ePressed)
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
            InventoryManager.Instance.Add(item);
            Destroy(gameObject);
            ClosePanel();
        }

        public void OpenPanel()
        {
            if (_panel != null) _panel.SetActive(true); // Aktivieren Sie das Panel
        }

        public void ClosePanel()
        {
            if (_panel != null) _panel.SetActive(false); // Aktivieren Sie das Panel
        }
    }
}