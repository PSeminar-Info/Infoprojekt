using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    private bool Triggeractive = false;
    


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Triggeractive = true;
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Triggeractive = false;
            CloseChest();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Triggeractive && Input.GetKeyDown(KeyCode.E))
        {
          
             OpenChest();
            Triggeractive = false;
        }
    }

     void OpenChest()
    {
        transform.GetChild(1).Rotate(-60, 0, 0);
        
        // Inventar öffnen
    }
    void CloseChest()
    {
        transform.GetChild(1).Rotate(60, 0, 0);
        // Inventar schließen
    }
        

}
