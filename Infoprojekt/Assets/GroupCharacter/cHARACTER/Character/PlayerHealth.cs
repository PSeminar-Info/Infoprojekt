using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider HealtBar;
    public Text TextHealth;
    public Slider ManaBar;
    public Text TextMana;
    public int Health = 100;
    public float mana;
    public bool animationn;
    public GameObject Panel;
    public bool dead;
    private Animator animator;
    public LayerMask layerToCheck; // Layer, den du überprüfen möchtest

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (mana < 100)
        {
            mana += 0.01f;
            ManaBar.value = mana;
            TextMana.text = "" + ManaBar.value;
        }
        else
        {
            mana = 100;
            ManaBar.value = mana;
            TextMana.text = "" + mana;
        }


        if (animationn)
        {
            //  animator.SetBool("Trank", true);
            Panel.SetActive(false);
            animationn = false;
        }

        //animator.SetBool("Trank", false);
        UpdateHealthBar();
        if (Health <= 0)
        {
            animator.SetBool("die", true);
            dead = true;
        }
        if(Health >= 100)
        {
            Health = 100;
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