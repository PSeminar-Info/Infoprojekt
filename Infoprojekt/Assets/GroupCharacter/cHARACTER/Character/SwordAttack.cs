using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float knockbackForce = 1000000f;
    public int damageAmount = 30;
    PlayerAttack playerAttack;
    public GameObject player;
    Animator animator;
    public bool canattack;
    void Start()
    {
        playerAttack = player.GetComponent<PlayerAttack>();
        animator = this.player.GetComponent<Animator>();
        canattack = false;
    }
    
    void Update()
    {
        if(playerAttack.GetAnimation("attack1"))
        {
            if (GetCurrentAnimationTime() < 0.5f)
            {
                canattack = true;
            }
            if ( GetCurrentAnimationTime() > 0.5f && canattack)
            {
                Attack();
                canattack = false;
            }

        }
    }

    public void Attack()
    {
        // Definiere die Position der unsichtbaren Kugel
        Vector3 spherePosition = this.transform.parent.position;

        // Überprüfe, ob sich in der unsichtbaren Kugel Objekte befinden
        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, attackRange);

        // Durchlaufe alle getroffenen Colliders
        foreach (var hitCollider in hitColliders)
        {
            // Überprüfe, ob das getroffene Objekt den Tag "enemy" hat
            if (hitCollider.CompareTag("enemy"))
            {
                ApplyDamageAndKnockback(hitCollider.gameObject);
            }
        }
    }

    public void ApplyDamageAndKnockback(GameObject enemy)
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
                Debug.Log("kk");
                // Wende die Kraft auf den Rigidbody an, um den Gegner wegzuschleudern
                enemyRigidbody.AddForce((Vector3.up + knockbackDirection.normalized) * knockbackForce, ForceMode.Impulse);
            }
        }
        else
        {
            // Füge hier zusätzlichen Code hinzu, um Schaden bei Objekten ohne Health-Skript zu verursachen
        }
    }
    float GetCurrentAnimationTime()
    {
        // Holen Sie sich Informationen über den aktuellen Zustand der Animation
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Berechne die verstrichene Zeit seit dem Beginn der Animation
        float currentTime = stateInfo.normalizedTime * stateInfo.length;

        return currentTime;
    }
}
