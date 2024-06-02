using UnityEngine;

namespace GroupCharacter.cHARACTER.Character
{
    public class PlayerAttack : MonoBehaviour
    {
        public float timetoattack = 0.5f;
        public float dammage;
        public GameObject specialattack;
        public float explosionRadius = 3f;
        public float explosionForce = 100f;
        public bool special;
        private Animator _animator;
        private bool _first;
        private float _timer;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            special = false;
            _first = true;
        }

        private void Update()
        {
            if (GetAnimation("attack3") && !special && _first)
            {
                special = true;
                _first = false;
            }

            if (!GetAnimation("attack3"))
            {
                special = false;
                _first = true;
            }

            if (special) _timer += Time.deltaTime;
            if (_timer > timetoattack)
            {
                var ins = transform.position + Vector3.up * 1;
                Instantiate(specialattack, ins, Quaternion.identity);
                _timer = 0;
                special = false;
                _first = false;
            }
        }

        public bool GetAnimation(string name)
        {
            // Überprüfe die aktuelle Animation
            var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            // Überprüfe, ob die Animation mit einem bestimmten Namen abgespielt wird
            if (stateInfo.IsName(name))
                return true;
            return false;
        }
    }
}