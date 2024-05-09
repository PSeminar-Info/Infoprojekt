using UnityEngine;

public class ToggleManager : MonoBehaviour
{

    public bool village;
    public bool forest;
    public bool graveyard;
    public bool castle;
    public bool lazylake;
    public bool fisherslake;
    public bool tundracastle;

    public void changevillage(bool change)
    {
        village = change;
    }

    public void changeforest(bool change)
    {
        forest = change;
    }

    public void changegraveyard(bool change)
    {
        graveyard = change;
    }

    public void changecastle(bool change)
    {
        castle = change;
    }

    public void changelazylake(bool change)
    {
        lazylake = change;
    }

    public void changefisherslake(bool change)
    {
        fisherslake = change;
    }

    public void changetundracastle(bool change)
    {
        tundracastle = change;
    }

    public string GetMap()
    {
        return village ? "village" : forest ? "forest" : graveyard ? "graveyard" : castle ? "castle" : lazylake ? "lazylake" : fisherslake ? "fisherslake" : tundracastle ? "tundracastle" : "Error";
    }
}