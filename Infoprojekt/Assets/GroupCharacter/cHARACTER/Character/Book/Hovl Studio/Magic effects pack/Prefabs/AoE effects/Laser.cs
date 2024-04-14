using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
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
        
    }
    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "enemy")
        {
            Debug.Log("abcd");
            swordattack.ApplyDamageAndKnockback(col.gameObject,damage);
        }
    }
}
