using UnityEngine;

public class ToggleManager : MonoBehaviour
{
    public GameObject toggleMountain;
    public GameObject toggleLake;
    public bool mountain;
    public bool lake;

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
        return mountain ? "mountain" : lake ? "lake" : "Error";
    }
}