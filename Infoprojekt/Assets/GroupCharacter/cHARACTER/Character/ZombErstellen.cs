using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombErstellen : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Zombie;
    public float x;
    public float y;
    public float z;
    private float timer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2 )
        {
            timer = 0;
            zombieserstellen();
        }
    }
    public void zombieserstellen()
    {

        Instantiate(Zombie, new Vector3(x, y, z), Quaternion.identity);

    }
}
