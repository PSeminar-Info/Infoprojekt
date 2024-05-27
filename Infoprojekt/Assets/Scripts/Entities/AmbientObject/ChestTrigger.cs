using UnityEngine;

public class Trigger : MonoBehaviour
{
    private bool Chestopen;

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
        if (other.CompareTag("Player"))
        { Triggeractive = true;
          
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { Triggeractive = false; }

        if (Chestopen) { CloseChest(); }
    }

    private void OpenChest()
    {
        transform.GetChild(1).Rotate(new Vector3(-60, 0, 0), Space.Self);
        transform.GetChild(1).Translate(new Vector4(0, 0.2f, 0f));
        Chestopen = true;

        // Inventar öffnen
    }

    private void CloseChest()
    {
        transform.GetChild(1).Rotate(new Vector3(60, 0, 0), Space.Self);
        transform.GetChild(1).Translate(new Vector4(0, -0.1f, 0.17f));
        Chestopen = false;
        // Inventar schließen
    }
}