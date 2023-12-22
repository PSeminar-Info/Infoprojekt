using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zomb : MonoBehaviour
{
    public float leben = 30;
    // Start is called before the first frame update
    public GameObject target;
    Animator animator;
    public bool gethit;
    PlayerHealth playerhealth;
    float timetilldead = 0;
    public float schaden;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerhealth = target.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leben >= 0)
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(target.transform.position);
        }
       
        
        else
        {

            GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            GetComponent<NavMeshAgent>().isStopped = true;
            animator.SetBool("die", true);

            timetilldead += Time.deltaTime;
            if (timetilldead >= 2)
            {

                Destroy(gameObject);
            }
        }
        if (gethit)
        {
            leben -= schaden;
            knockback();
        }
        
        




    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("attack", true);
            playerhealth.Health -= 2;
        }

    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("attack", false);
        }

    }
    public void knockback()
    {
        gethit = false;
        Vector3 back = new Vector3(0, 9, -3) * 1;
        this.transform.Translate(back);
    }



}


