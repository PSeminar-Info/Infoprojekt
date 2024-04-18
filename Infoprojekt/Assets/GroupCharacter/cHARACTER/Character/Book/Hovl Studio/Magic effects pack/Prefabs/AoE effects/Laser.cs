using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float damage;
    private SwordAttack swordattack;

    private void Start()
    {
        player = GameObject.FindWithTag("sword");

        swordattack = player.GetComponent<SwordAttack>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "enemy")
        {
            Debug.Log("abcd");
            swordattack.ApplyDamageAndKnockback(col.gameObject, damage);
        }
    }
}