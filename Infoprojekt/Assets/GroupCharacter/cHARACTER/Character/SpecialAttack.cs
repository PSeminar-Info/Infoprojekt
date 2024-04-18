using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 1000000f;
    private readonly int damageAmount = 50;

    private void Start()
    {
        Explode();
    }

    private void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var collider in colliders)
        {
            var rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Berechne die Richtung vom Zentrum der Explosion zum Collider
                var explosionDirection = collider.transform.position - transform.position;

                // Berechne die Explosionkraft basierend auf der Entfernung zum Zentrum der Explosion
                var distance = explosionDirection.magnitude;
                var falloff = 1 - distance / explosionRadius;
                var force = Mathf.Max(0, falloff) * explosionForce;

                // Wende die Kraft auf den Rigidbody an
                rb.AddForce((Vector3.up + explosionDirection.normalized) * force, ForceMode.Impulse);

                var enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                    // Wende Schaden an
                    enemyHealth.TakeDamage(damageAmount);
            }
        }
    }
}