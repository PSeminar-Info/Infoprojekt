using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        // Füge hier weitere Aktionen hinzu, die bei Tod des Gegners ausgeführt werden sollen
        Destroy(gameObject);
    }
}