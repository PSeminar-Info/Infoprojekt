using UnityEngine;

namespace GroupCharacter.cHARACTER.Character
{
    public class EnemyHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0) Die();
        }

        private void Die()
        {
            // Füge hier weitere Aktionen hinzu, die bei Tod des Gegners ausgeführt werden sollen
            Destroy(gameObject);
        }
    }
}