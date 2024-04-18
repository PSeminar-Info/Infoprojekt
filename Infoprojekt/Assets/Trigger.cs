using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    private bool Triggeractive;

    // Update is called once per frame
    private void Update()
    {
        if (Triggeractive && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
            Triggeractive = false;
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Triggeractive = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Triggeractive = false;
            CloseChest();
        }
    }

    private void OpenChest()
    {
        transform.GetChild(1).Rotate(-60, 0, 0);

        // Inventar öffnen
    }

    private void CloseChest()
    {
        transform.GetChild(1).Rotate(60, 0, 0);
        // Inventar schließen
    }
}