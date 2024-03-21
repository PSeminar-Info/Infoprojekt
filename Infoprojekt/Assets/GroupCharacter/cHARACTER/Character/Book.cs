using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    // Start is called before the first frame update
    public bool attackFinished;
    private float cooldown;
    void Start()
    {
        attackFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!attackFinished)
        {
            cooldown += Time.deltaTime;
        }
        if(cooldown > 3)
        {
            attackFinished = true;
            cooldown = 0;
        }
    }
    public void AttackBook(int number)
    {
        switch (number)
        {
            case 1:
                Debug.Log("Fall 1 wurde ausgelöst.");
                // Füge hier die Aktionen für Fall 1 hinzu

                break;
            case 2:
                Debug.Log("Fall 2 wurde ausgelöst.");
                // Füge hier die Aktionen für Fall 2 hinzu

                break;
            case 3:
                Debug.Log("Fall 3 wurde ausgelöst.");
                // Füge hier die Aktionen für Fall 3 hinzu

                break;
            
        }
    }
}
