using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerHealth playerhealth;
    private float timer;
    public GameObject HealAnimation;
    private GameObject currentHealAnimation; // Speichert das aktuelle HealAnimation-Objekt

    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider)
    {
        if (currentHealAnimation != null)
        {
            currentHealAnimation.gameObject.transform.SetParent(null); // Oder: transform.parent = null;
            Destroy(currentHealAnimation.gameObject);
        }
        currentHealAnimation = Instantiate(HealAnimation, collider.gameObject.transform.position, Quaternion.identity);
        currentHealAnimation.gameObject.transform.SetParent(collider.gameObject.transform);

    }
    void OnTriggerStay(Collider collider) 
    { 
        if(collider.gameObject.tag == "Player")
        {

            timer += Time.deltaTime;
            if(timer >= 1)
            {
                playerhealth = collider.GetComponent<PlayerHealth>();
                playerhealth.Health += 2;
                timer = 0;

            }
           

        }
    }
    void OnTriggerExit(Collider collider)
    {
        timer = 0;
        if (currentHealAnimation != null)
        {
            currentHealAnimation.gameObject.transform.SetParent(null); // Oder: transform.parent = null;
            Destroy(currentHealAnimation.gameObject);
        }

    }

}
