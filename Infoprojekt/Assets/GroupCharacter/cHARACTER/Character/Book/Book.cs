using UnityEngine;

public class Book : MonoBehaviour
{
    // Start is called before the first frame update
    public bool attackFinished;
    public GameObject book;
    public GameObject bookPoint;
    public Transform parent;
    public GameObject Animation1;
    public GameObject Animation2;
    public GameObject Animation3;
    public bool playerEffect;
    private float cooldown;
    private GeneralEffectScript geneff;
    private float spawnDistance;

    private void Start()
    {
        spawnDistance = 10;
        attackFinished = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!attackFinished) cooldown += Time.deltaTime;
        if (cooldown > 2)
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
                Ins(spawnDistance, Animation1);
                break;
            case 2:
                //Debug.Log("Fall 2 wurde ausgelöst.");

                // Füge hier die Aktionen für Fall 2 hinzu
                geneff = Animation2.GetComponent<GeneralEffectScript>();
                spawnDistance = geneff.distance;
                Ins(spawnDistance, Animation2);
                break;
            case 3:
                //Debug.Log("Fall 3 wurde ausgelöst.");

                // Füge hier die Aktionen für Fall 3 hinzu
                geneff = Animation3.GetComponent<GeneralEffectScript>();
                spawnDistance = geneff.distance;
                Ins(spawnDistance, Animation3);
                break;
        }
    }

    public void Ins(float dis, GameObject a)
    {
        Instantiate(book, bookPoint.transform.position, Quaternion.identity);
        // Position des Spielers erhalten
        var playerPosition = transform.position;

        // Richtung vom Spieler zum Spawnpunkt berechnen
        var spawnDirection = transform.forward;

        // Berechnen der Spawnposition um spawnDistance vor dem Spieler
        var positiontospawn = playerPosition + spawnDirection * dis;

        Instantiate(a, positiontospawn, Quaternion.identity);
    }
}