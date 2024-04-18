using Entities.Npc.Enemy.Wizard;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public bool shoot;
    public float force;
    public float knockbackForce = 1000000f;
    public int damageAmount = 10;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Überprüfe, ob der Spieler mit dem Boden kollidiert
        if (collision.gameObject.tag != "Player")
            //Destroy(this.gameObject);
            rb.isKinematic = true;
        if (collision.gameObject.tag == "enemy") ApplyDamageAndKnockback(collision.gameObject);
    }

    public void Shoot(float force)
    {
        // Richtung des Objekts verwenden (y-Komponente ignorieren, um flach zu bleiben)
        var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        var ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("hg" + hit.transform.gameObject);
            transform.LookAt(hit.point);
        }

        // Hier hast du die Blickrichtung vom Mittelpunkt des Bildschirms aus
        var kraftRichtung = transform.forward;
        ;
        // Kraft zum Rigidbody hinzufügen
        rb.AddForce(kraftRichtung * force, ForceMode.Impulse);
        transform.Rotate(new Vector3(90, 0, 0));
    }

    public void ApplyDamageAndKnockback(GameObject enemy)
    {
        // Überprüfe, ob das betroffene Objekt ein Skript für die Verwaltung von Lebenspunkten hat
        var enemyHealth = enemy.GetComponent<WizardController>();
        var enemyRigidbody = enemy.GetComponent<Rigidbody>();

        if (enemyHealth != null)
        {
            // Wende Schaden an
            enemyHealth.TakeDamage(damageAmount);

            // Schleudere den Gegner weg
            if (enemyRigidbody != null)
            {
                // Berechne die Richtung vom Schwert zum Gegner
                var knockbackDirection = enemy.transform.position - transform.position;
                Debug.Log("kk");
                // Wende die Kraft auf den Rigidbody an, um den Gegner wegzuschleudern
                enemyRigidbody.AddForce((Vector3.up + knockbackDirection.normalized) * knockbackForce,
                    ForceMode.Impulse);
            }
        }
        // Füge hier zusätzlichen Code hinzu, um Schaden bei Objekten ohne Health-Skript zu verursachen
    }
}