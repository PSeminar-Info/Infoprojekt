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
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("hg" + hit.point);
            this.transform.LookAt(hit.point);
        }
        // Hier hast du die Blickrichtung vom Mittelpunkt des Bildschirms aus
        Vector3 kraftRichtung = this.transform.forward; ;
        // Kraft zum Rigidbody hinzufügen
        rb.AddForce(kraftRichtung * force, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Überprüfe, ob der Spieler mit dem Boden kollidiert
        if (collision.gameObject.tag == "floor")
        {
            //Destroy(this.gameObject);
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        }
    }
}