using UnityEngine;

namespace GroupCharacter.cHARACTER.Character
{
    public class SwordAttack : MonoBehaviour
    {
        public float attackRange = 2f;
        public float knockbackForce = 1000000000000f;
        public int damageAmount = 3;
        public GameObject player;
        public bool canattack;
        private Animator _animator;
        private PlayerAttack _playerAttack;

        private void Start()
        {
            _playerAttack = player.GetComponent<PlayerAttack>();
            _animator = player.GetComponent<Animator>();
            canattack = false;
        }

        private void Update()
        {
            if (_playerAttack.GetAnimation("atack2"))
            {
                if (GetCurrentAnimationTime() < 0.7f) canattack = true;
                if (GetCurrentAnimationTime() > 0.7f && canattack)
                {
                    Attack();
                    canattack = false;
                }
            }
        }

        public void Attack()
        {
            // Definiere die Position der unsichtbaren Kugel
            var spherePosition = transform.parent.position;

            // Überprüfe, ob sich in der unsichtbaren Kugel Objekte befinden
            var hitColliders = Physics.OverlapSphere(spherePosition, attackRange);

            // Durchlaufe alle getroffenen Colliders
            foreach (var hitCollider in hitColliders)
                // Überprüfe, ob das getroffene Objekt den Tag "enemy" hat
                if (hitCollider.CompareTag("enemy"))
                {
                    Debug.Log("müsstegehen");

                    ApplyDamageAndKnockback(hitCollider.gameObject);
                }
        }

        public void ApplyDamageAndKnockback(GameObject enemy)
        {
            // Überprüfe, ob das betroffene Objekt ein Skript für die Verwaltung von Lebenspunkten hat

            //wizardController = enemy.GetComponent<WizardController>();
            var enemyRigidbody = enemy.GetComponent<Rigidbody>();

            //if (wizardController != null)
            // {
            //  wizardController.TakeDamage(damageAmount);

            // Schleudere den Gegner weg
            if (enemyRigidbody != null)
            {
                // Berechne die Richtung vom Schwert zum Gegner
                var knockbackDirection = enemy.transform.position - transform.position;
                Debug.Log("kk");
                // Wende die Kraft auf den Rigidbody an, um den Gegner wegzuschleudern
                enemyRigidbody.AddForce((Vector3.up + knockbackDirection.normalized) * knockbackForce, ForceMode.Impulse);
            }
            // }
        }

        private float GetCurrentAnimationTime()
        {
            // Holen Sie sich Informationen über den aktuellen Zustand der Animation
            var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            // Berechne die verstrichene Zeit seit dem Beginn der Animation
            var currentTime = stateInfo.normalizedTime * stateInfo.length;

            return currentTime;
        }
    }
}