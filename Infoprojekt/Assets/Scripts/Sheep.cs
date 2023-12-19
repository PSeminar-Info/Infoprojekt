using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Npc
{
    // Start is called before the first frame update
    void Start()
    {
        Health = 10; 
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Move randomly, move to Player sometimes to "say hello", eat grass
        MoveBy(-5, 0);
    }
}
