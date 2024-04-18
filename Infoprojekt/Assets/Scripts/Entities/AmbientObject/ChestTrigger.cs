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
        if (other.CompareTag("Player")) Triggeractive = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) Triggeractive = false;

        if (Chestopen) CloseChest();
    }

    private void OpenChest()
    {
        transform.GetChild(1).RotateAround(transform.GetChild(3).position, Vector3.left, 60);
        Chestopen = true;

        // Inventar öffnen
    }

    private void CloseChest()
    {
        transform.GetChild(1).RotateAround(transform.GetChild(3).position, Vector3.left, -60);
        Chestopen = false;
        // Inventar schließen
    }
}