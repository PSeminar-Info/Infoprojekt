using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider HealtBar;
    public Text TextHealth;
    Animator animator;
    public int Health = 100;
    public bool animationn = false;
    public GameObject Panel;
    public bool dead;
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (animationn)
        {
          //  animator.SetBool("Trank", true);
            Panel.SetActive(false);
            animationn = false;
        }
        else
        {
            //animator.SetBool("Trank", false);
        }

        UpdateHealthBar();
        if(Health <= 0)
        {
            animator.SetBool("die",true);
            dead = true;
        }
    }
    // public void TakeDamage()
    // {
    //     Health -= 20;
    //     if(Health < 0 )
    //     {
    //         Health = 0;
    //     }  
    //     UpdateHealthBar();
    // }
    public void UpdateHealthBar()
    {
        HealtBar.value = Health;
        TextHealth.text = "" + Health;
    }
    

}
