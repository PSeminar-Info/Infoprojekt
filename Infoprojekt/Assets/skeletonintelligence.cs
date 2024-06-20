using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    public NavMeshAgent agent;
    // public GameObject attack;

    public float hitPoints;

    public Transform player;

    public LayerMask groundLevel;
    public LayerMask playerLevel;

    public Vector3 patrolPoint;
    public float patrolRange;


    public float attackCooldown;


    public float chaseRange, attackRange;
    public bool playerInChaseRange = false;
    public bool playerInAttackRange = false;
    private bool _alreadyAttacked;
    private bool _patrolPointSet;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, playerLevel);
        // playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLevel);
        


        if (!playerInChaseRange && !playerInAttackRange) Patroling();
        if (playerInChaseRange && !playerInAttackRange) Chasing();
        // if (playerInChaseRange && playerInAttackRange && )) Death();
        if (Input.GetKeyDown(KeyCode.Z)) Death();
    }

    private void Patroling()
    {
        if (!_patrolPointSet) SearchPatrolPoint();

        if (_patrolPointSet)
            agent.SetDestination(patrolPoint);

        var distanceToPatrolPoint = transform.position - patrolPoint;

        if (distanceToPatrolPoint.magnitude < 1f)
            _patrolPointSet = false;
    }

    private void SearchPatrolPoint()
    {
        var randomZ = Random.Range(-patrolRange, patrolRange);
        var randomX = Random.Range(-patrolRange, patrolRange);

        patrolPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (_alreadyAttacked) return;
        //Instantiate(attack, transform);


        _alreadyAttacked = true;
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    public void Ouch(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0) Death();
    }

    private void Death()
    {
        Destroy(gameObject, 2f);
    }
}