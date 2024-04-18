using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float timetoattack = 0.5f;
    public float dammage;
    public GameObject specialattack;
    public float explosionRadius = 3f;
    public float explosionForce = 100f;
    public bool special;
    private Animator animator;
    private bool first;
    private float timer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        special = false;
        first = true;
    }

    private void Update()
    {
        if (GetAnimation("attack3") && !special && first)
        {
            special = true;
            first = false;
        }

        if (!GetAnimation("attack3"))
        {
            special = false;
            first = true;
        }

        if (special) timer += Time.deltaTime;
        if (timer > timetoattack)
        {
            var ins = transform.position + Vector3.up * 1;
            Instantiate(specialattack, ins, Quaternion.identity);
            timer = 0;
            special = false;
            first = false;
        }
    }

    public bool GetAnimation(string name)
    {
        // Überprüfe die aktuelle Animation
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Überprüfe, ob die Animation mit einem bestimmten Namen abgespielt wird
        if (stateInfo.IsName(name))
            return true;
        return false;
    }
}