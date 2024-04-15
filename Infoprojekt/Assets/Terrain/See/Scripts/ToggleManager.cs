using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{
    public GameObject toggleMountain;
    public GameObject toggleLake;
    public bool mountain = false;
    public bool lake = false;
    
    public void changemountain(bool change)
    {
        mountain = change;
    }

    public void changelake(bool change)
    {
        lake = change;
    }

    public string GetMap()
    {
        return mountain ? "mountain" : (lake ? "lake" : "Error");
    }
}
