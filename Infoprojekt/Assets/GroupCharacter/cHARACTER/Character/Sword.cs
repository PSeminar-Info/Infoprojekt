using UnityEngine;

public class SwordController : MonoBehaviour
{
    public float knockbackForce = 10f;
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfe, ob der Kollider des Schwerts mit einem Objekt kollidiert
        // Du könntest auch Layer verwenden, um sicherzustellen, dass nur bestimmte Objekte betroffen sind
        if (other.CompareTag("Enemy"))
        {
            // Rufe die Methode zum Anwenden von Schaden auf dem betroffenen Objekt auf
            ApplyDamageAndKnockback(other.gameObject);
        }
    }

    void ApplyDamageAndKnockback(GameObject enemy)
    {
        // Überprüfe, ob das betroffene Objekt ein Skript für die Verwaltung von Lebenspunkten hat
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();

        if (enemyHealth != null)
        {
            // Wende Schaden an
            enemyHealth.TakeDamage(damageAmount);

            // Schleudere den Gegner weg
            if (enemyRigidbody != null)
            {
                // Berechne die Richtung vom Schwert zum Gegner
                Vector3 knockbackDirection = enemy.transform.position - transform.position;

                // Wende die Kraft auf den Rigidbody an, um den Gegner wegzuschleudern
                enemyRigidbody.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode.Impulse);
            }
        }
        else
        {
            // Füge hier zusätzlichen Code hinzu, um Schaden bei Objekten ohne Health-Skript zu verursachen
        }
    }
}
