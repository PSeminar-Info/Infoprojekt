using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedThing : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    private bool h = true;
    public GameObject player;
    SwordAttack swordattack;
    public float damage;
    void Start()
    {
        player = GameObject.FindWithTag("sword");
        swordattack = player.GetComponent<SwordAttack>();

    }

    // Update is called once per frame
    void Update()
    {
        if(h)
        {
            timer += Time.deltaTime;

        }

    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            if (timer >= 1)
            {
                Debug.Log("abchabddei");
                timer = 0;
                h = false;
                swordattack.ApplyDamageAndKnockback(other.gameObject, damage);

            }

        }
       
    }
}
