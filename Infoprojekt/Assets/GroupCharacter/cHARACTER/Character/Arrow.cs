using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public bool shoot;
    public float force;
    public float knockbackForce = 1000000f;
    public int damageAmount = 10;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shoot(float force)
    {
        // Richtung des Objekts verwenden (y-Komponente ignorieren, um flach zu bleiben)
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.tag == "enemy")
            {
                Debug.Log("hg" + hit.point);
                this.transform.LookAt(hit.point);
            }
            
        }
        
        // Hier hast du die Blickrichtung vom Mittelpunkt des Bildschirms aus
        Vector3 kraftRichtung = this.transform.forward; ;
        // Kraft zum Rigidbody hinzufügen
        rb.AddForce(kraftRichtung * force, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider collision)
    {
        // Überprüfe, ob der Spieler mit dem Boden kollidiert
        if (collision.gameObject.tag == "floor")
        {
            //Destroy(this.gameObject);
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        }
        if(collision.gameObject.tag == "enemy")
        {
            ApplyDamageAndKnockback(collision.gameObject);

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
}