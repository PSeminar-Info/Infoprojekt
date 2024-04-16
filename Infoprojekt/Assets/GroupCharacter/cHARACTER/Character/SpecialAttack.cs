using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 1000000f;
    private int damageAmount = 50;
    void Start()
    {
        Explode();
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Berechne die Richtung vom Zentrum der Explosion zum Collider
                Vector3 explosionDirection = collider.transform.position - transform.position;

                // Berechne die Explosionkraft basierend auf der Entfernung zum Zentrum der Explosion
                float distance = explosionDirection.magnitude;
                float falloff = 1 - (distance / explosionRadius);
                float force = Mathf.Max(0, falloff) * explosionForce;

                // Wende die Kraft auf den Rigidbody an
                rb.AddForce((Vector3.up + explosionDirection.normalized) * force, ForceMode.Impulse);

                EnemyHealth enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    // Wende Schaden an
                    enemyHealth.TakeDamage(damageAmount);
                }
            }
        }
    }
}
