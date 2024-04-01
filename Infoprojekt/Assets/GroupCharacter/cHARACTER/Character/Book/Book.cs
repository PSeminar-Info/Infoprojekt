using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    // Start is called before the first frame update
    public bool attackFinished;
    private float cooldown;
    public GameObject book;
    public GameObject bookPoint;
    public Transform parent;
    public GameObject Animation1;
    public GameObject Animation2;
    public GameObject Animation3;
    private float spawnDistance;
    public bool playerEffect;
    GeneralEffectScript geneff;
    void Start()
    {
        spawnDistance = 10;
        attackFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!attackFinished)
        {
            cooldown += Time.deltaTime;
        }
        if(cooldown > 2)
        {
            attackFinished = true;
            cooldown = 0;
        }
    }
    public void AttackBook(int number)
    {
        
        
        
        switch (number)
        {
            case 1:
                //Debug.Log("Fall 1 wurde ausgelöst.");
                // Füge hier die Aktionen für Fall 1 hinzu
                geneff = Animation1.GetComponent<GeneralEffectScript>();
                spawnDistance = geneff.distance;
                Ins(spawnDistance,Animation1);
                break;
            case 2:
                //Debug.Log("Fall 2 wurde ausgelöst.");

                // Füge hier die Aktionen für Fall 2 hinzu
                geneff = Animation2.GetComponent<GeneralEffectScript>();
                spawnDistance = geneff.distance;
                Ins(spawnDistance,Animation2);
                break;
            case 3:
                //Debug.Log("Fall 3 wurde ausgelöst.");

                // Füge hier die Aktionen für Fall 3 hinzu
                geneff = Animation3.GetComponent<GeneralEffectScript>();
                spawnDistance = geneff.distance;
                Ins(spawnDistance,Animation3);
                break;
            
        }
    }
    public void Ins(float dis,GameObject a)
    {
        Instantiate(book, bookPoint.transform.position, Quaternion.identity);
        // Position des Spielers erhalten
        Vector3 playerPosition = this.transform.position;

        // Richtung vom Spieler zum Spawnpunkt berechnen
        Vector3 spawnDirection = this.transform.forward;

        // Berechnen der Spawnposition um spawnDistance vor dem Spieler
        Vector3 positiontospawn = playerPosition + spawnDirection * dis;

        Instantiate(a, positiontospawn, Quaternion.identity);

    }
}
