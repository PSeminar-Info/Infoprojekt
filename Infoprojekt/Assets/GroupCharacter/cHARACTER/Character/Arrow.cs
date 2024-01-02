using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public bool shoot;
    public float force;
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
        Vector3 kraftRichtung = -transform.up;

        // Kraft zum Rigidbody hinzufügen
        rb.AddForce(kraftRichtung * force, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Überprüfe, ob der Spieler mit dem Boden kollidiert
        if (collision.gameObject.tag == "floor")
        {
            Destroy(this.gameObject);

        }
    }
}