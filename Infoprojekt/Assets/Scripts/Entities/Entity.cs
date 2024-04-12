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
            // TODO: spawn animation
        }

        private static void OnDeath()
        {
            // implement in child class
        }

        public void OnDespawn()
        {
            // TODO: despawn behaviour
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
            // TODO: damage animation

            health -= amount;
            if (health <= 0) Die();
        }
    }
}