using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public float health;
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange;
    public float attackRange;
    public int damage;
    public Animator animator;
    public ParticleSystem hitEffect;

    private Vector3 _walkPoint;
    private bool _walkPointSet;
    private bool _alreadyAttacked;
    private bool _takeDamage;
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Velocity = Animator.StringToHash("Velocity");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        var playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        var playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        switch (playerInSightRange)
        {
            case false when !playerInAttackRange:
                Patrolling();
                break;
            case true when !playerInAttackRange:
                ChasePlayer();
                break;
            default:
            {
                if (playerInSightRange)
                {
                    AttackPlayer();
                }
                else if (_takeDamage)
                {
                    ChasePlayer();
                }

                break;
            }
        }
    }

    private void Patrolling()
    {
        if (!_walkPointSet)
        {
            SearchWalkPoint();
        }

        if (_walkPointSet)
        {
            navAgent.SetDestination(_walkPoint);
        }

        var distanceToWalkPoint = transform.position - _walkPoint;
        animator.SetFloat(Velocity, 0.2f);

        if (distanceToWalkPoint.magnitude < 1f)
        {
            _walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        var randomZ = Random.Range(-walkPointRange, walkPointRange);
        var randomX = Random.Range(-walkPointRange, walkPointRange);
        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, groundLayer))
        {
            _walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        navAgent.SetDestination(player.position);
        animator.SetFloat(Velocity, 0.6f);
        navAgent.isStopped = false; // Add this line
    }


    private void AttackPlayer()
    {
        navAgent.SetDestination(transform.position);

        if (_alreadyAttacked) return;
        transform.LookAt(player.position);
        _alreadyAttacked = true;
        animator.SetBool(Attack, true);
        Invoke(nameof(ResetAttack), timeBetweenAttacks);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            /*
                    YOU CAN USE THIS TO GET THE PLAYER HUD AND CALL THE TAKE DAMAGE FUNCTION

                PlayerHUD playerHUD = hit.transform.GetComponent<PlayerHUD>();
                if (playerHUD != null)
                {
                   playerHUD.takeDamage(damage);
                }
                 */
        }
    }


    private void ResetAttack()
    {
        _alreadyAttacked = false;
        animator.SetBool(Attack, false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hitEffect.Play();
        StartCoroutine(TakeDamageCoroutine());

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private IEnumerator TakeDamageCoroutine()
    {
        _takeDamage = true;
        yield return new WaitForSeconds(2f);
        _takeDamage = false;
    }

    private void DestroyEnemy()
    {
        StartCoroutine(DestroyEnemyCoroutine());
    }

    private IEnumerator DestroyEnemyCoroutine()
    {
        animator.SetBool(Dead, true);
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}