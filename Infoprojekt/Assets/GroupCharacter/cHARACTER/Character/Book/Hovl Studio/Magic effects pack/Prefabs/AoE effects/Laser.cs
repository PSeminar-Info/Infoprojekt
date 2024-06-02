using UnityEngine;

namespace GroupCharacter.cHARACTER.Character.Book.Hovl_Studio.Magic_effects_pack.Prefabs.AoE_effects
{
    public class Laser : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject player;
        public float damage;
        private SwordAttack _swordattack;

        private void Start()
        {
            player = GameObject.FindWithTag("sword");

            _swordattack = player.GetComponent<SwordAttack>();
        }

        private void OnTriggerStay(Collider col)
        {
            if (!col.gameObject.CompareTag("enemy")) return;
            Debug.Log("abcd");
            _swordattack.ApplyDamageAndKnockback(col.gameObject, damage);
        }
    }
}