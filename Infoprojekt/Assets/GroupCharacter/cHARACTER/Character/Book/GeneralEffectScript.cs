using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEffectScript : MonoBehaviour
{
    public float distance;
    public float timetilldeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
            Destroy(this.gameObject,timetilldeath);
        
    }
}
