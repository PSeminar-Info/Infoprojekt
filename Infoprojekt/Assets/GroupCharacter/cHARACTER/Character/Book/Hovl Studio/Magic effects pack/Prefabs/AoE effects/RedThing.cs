using UnityEngine;

public class RedThing : MonoBehaviour
{
    public GameObject player;
    public float damage;
    private bool h = true;

    private SwordAttack swordattack;

    // Start is called before the first frame update
    private float timer;

    private void Start()
    {
        player = GameObject.FindWithTag("sword");
        swordattack = player.GetComponent<SwordAttack>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (h) timer += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy")
            if (timer >= 1)
            {
                Debug.Log("abchabddei");
                timer = 0;
                h = false;
                swordattack.ApplyDamageAndKnockback(other.gameObject, damage);
            }
    }
}