using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private bool Triggeractive = false;
    private bool Chestopen = false;


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
            
        }

        if (Chestopen == true)
        {
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
        transform.GetChild(1).RotateAround(transform.GetChild(3).position,Vector3.left,60);
        Chestopen = true;
        
        // Inventar öffnen
    }
    void CloseChest()
    {
        transform.GetChild(1).RotateAround(transform.GetChild(3).position, Vector3.left, -60);
        Chestopen = false;
        // Inventar schließen
    }
        

}
