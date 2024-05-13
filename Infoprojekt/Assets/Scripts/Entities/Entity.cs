using ScriptableObjects.Inventory.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public InventoryObject inventory;
        [FormerlySerializedAs("IsInvincible")] public bool isInvincible;
        [FormerlySerializedAs("Health")] public float health = 1;
        [FormerlySerializedAs("MaxHealth")] public float maxHealth = 1;

        private void Start()
        {
            health = maxHealth;
        }

        private void Update()
        {
            if (health <= 0)
            {
                OnDeath();
            }
        }

        private void OnDeath()
        {
            // implement in child class
            Destroy(gameObject);
        }

        public void OnDespawn()
        {
            // implement in child class
        }

        private void Die()
        {
            health = 0;
            OnDeath();
        }

        public void Heal(float amount)
        {
            health += amount;
            if (health > maxHealth) health = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (isInvincible) return;
            // implement damage animation in child class

            health -= amount;
            if (health <= 0) Die();
        }
    }
}