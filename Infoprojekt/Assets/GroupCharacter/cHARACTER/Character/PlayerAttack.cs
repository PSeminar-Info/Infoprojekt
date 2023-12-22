using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   
    public float  timetoattack = 0.5f;
    float timer = 0 ;
    public float dammage;
    public GameObject specialattack;
    public float explosionRadius = 3f;
    public float explosionForce = 100f;
    Animator animator;
    public bool special;
    private bool first;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        special = false;
        first = true;
    }
    void Update()
    {
        
       
        if(GetAnimation("attack3") && !special && first)
        {
            special = true;
            first = false;
        }
        if (!GetAnimation("attack3"))
        {
            special = false;
            first = true;
        }
        if (special)
        {
            timer += Time.deltaTime;

        }
        if (timer > timetoattack)
        {
            Vector3 ins = this.transform.position + Vector3.up * 1;
            Instantiate(specialattack,ins, Quaternion.identity);
            timer = 0;
            special = false;
            first = false;
        }
    }
    public bool GetAnimation(string name)
    {
        // Überprüfe die aktuelle Animation
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Überprüfe, ob die Animation mit einem bestimmten Namen abgespielt wird
        if (stateInfo.IsName(name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    
   
   

}
