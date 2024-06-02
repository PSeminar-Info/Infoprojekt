using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GroupCharacter.cHARACTER.Character
{
    public class PlayerHealth : MonoBehaviour
    {
        // Start is called before the first frame update
        [FormerlySerializedAs("HealtBar")] public Slider healtBar;
        [FormerlySerializedAs("TextHealth")] public Text textHealth;
        [FormerlySerializedAs("ManaBar")] public Slider manaBar;
        [FormerlySerializedAs("TextMana")] public Text textMana;
        [FormerlySerializedAs("Health")] public int health = 100;
        public float mana;
        public bool animationn;
        [FormerlySerializedAs("Panel")] public GameObject panel;
        public bool dead;
        private Animator _animator;
        public LayerMask layerToCheck; // Layer, den du überprüfen möchtest
        private static readonly int Die = Animator.StringToHash("die");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (mana < 100)
            {
                mana += 0.001f;
                manaBar.value = mana;
                textMana.text = "" + manaBar.value;
            }
            else
            {
                mana = 100;
                manaBar.value = mana;
                textMana.text = "" + mana;
            }


            if (animationn)
            {
                //  animator.SetBool("Trank", true);
                panel.SetActive(false);
                animationn = false;
            }

            //animator.SetBool("Trank", false);
            UpdateHealthBar();
            if (health <= 0)
            {
                _animator.SetBool(Die, true);
                dead = true;
            }
            if(health >= 100)
            {
                health = 100;
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
            healtBar.value = health;
            textHealth.text = "" + health;
        }

   
    }
}