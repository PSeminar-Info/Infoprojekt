using UnityEngine;

namespace GroupCharacter.cHARACTER.Character.Book.Hovl_Studio.Magic_effects_pack.Prefabs.AoE_effects
{
    public class RedThing : MonoBehaviour
    {
        public GameObject player;
        public float damage;
        private bool _h = true;

        private SwordAttack _swordattack;

        // Start is called before the first frame update
        private float _timer;

        private void Start()
        {
            player = GameObject.FindWithTag("sword");
            _swordattack = player.GetComponent<SwordAttack>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_h) _timer += Time.deltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("enemy"))
                if (_timer >= 1)
                {
                    Debug.Log("abchabddei");
                    _timer = 0;
                    _h = false;
                    _swordattack.ApplyDamageAndKnockback(other.gameObject, damage);
                }
        }
    }
}