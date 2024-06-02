using UnityEngine;

namespace Entities.AmbientObject
{
    public class Trigger : MonoBehaviour
    {
        private bool _chestOpen;

        private bool _triggerActive;

        // Update is called once per frame
        private void Update()
        {
            if (_triggerActive && Input.GetKeyDown(KeyCode.E))
            {
                OpenChest();
                _triggerActive = false;
            }
        }


        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            { _triggerActive = true;
          
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) { _triggerActive = false; }

            if (_chestOpen) { CloseChest(); }
        }

        private void OpenChest()
        {
            transform.GetChild(1).Rotate(new Vector3(-60, 0, 0), Space.Self);
            transform.GetChild(1).Translate(new Vector4(0, 0.2f, 0f));
            _chestOpen = true;

            // Inventar öffnen
        }

        private void CloseChest()
        {
            transform.GetChild(1).Rotate(new Vector3(60, 0, 0), Space.Self);
            transform.GetChild(1).Translate(new Vector4(0, -0.1f, 0.17f));
            _chestOpen = false;
            // Inventar schließen
        }
    }
}