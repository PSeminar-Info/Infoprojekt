using UnityEngine;
using UnityEngine.AI;


public class Skeleton : MonoBehaviour
{
    public NavMeshAgent agent;
   // public GameObject attack;

    public float hitPoints;

    public Transform player;

    public LayerMask groundLevel, playerLevel;

    public Vector3 patrolPoint;
    bool patrolPointSet;
    public float patrolRange;


    public float attackCooldown;
    bool alreadyAttacked;


    public float chaseRange, attackRange;
    public bool playerInChaseRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, playerLevel);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLevel);


        if (!playerInChaseRange && !playerInAttackRange) Patroling();
        if (playerInChaseRange && !playerInAttackRange) Chasing();
        if (playerInChaseRange && playerInAttackRange) Attacking();
    }

    private void Patroling() 
    {
        if (!patrolPointSet) SearchPatrolPoint();

        if (patrolPointSet)
            agent.SetDestination(patrolPoint);

        Vector3 distanceToPatrolPoint = transform.position - patrolPoint;

        if (distanceToPatrolPoint.magnitude < 1f)
            patrolPointSet = false;
    }

    private void SearchPatrolPoint()
    {
        float randomZ = Random.Range(-patrolRange, patrolRange);
        float randomX = Random.Range(-patrolRange, patrolRange);

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

        if(!alreadyAttacked)
        {

            //Instantiate(attack, transform);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),attackCooldown);

        }
    }

    private void ResetAttack() 
    {
        alreadyAttacked = false;
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

