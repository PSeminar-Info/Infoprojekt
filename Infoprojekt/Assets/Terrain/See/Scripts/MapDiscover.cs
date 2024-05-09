using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDiscover : MonoBehaviour
{

    public bool forest = false;
    public bool graveyard = false; 
    public bool castle = false;
    public bool lazylake = false;

    private void OnTriggerEnter(Collider other)
    {
        //checkt den Tag des Colliders und setzt dann die discover bool auf true
        switch (this.tag)
        {
            case "forest":
                forest = true;
                break;
            case "graveyard":
                graveyard = true;
                break;
            case "castle":
                castle = true;
                break;
        }
    }

    public bool ReturnDiscover(string map)
    {
        switch (map)
        {
            case "forest":
                return forest;
            case "graveyard":
                return graveyard;
            case "castle":
                return castle;
        }

        return false;
    }
}
