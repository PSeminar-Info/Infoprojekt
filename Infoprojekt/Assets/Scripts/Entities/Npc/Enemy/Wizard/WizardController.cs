using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Entities.Npc.Enemy.Wizard
{
    public class WizardController : Npc
    // the animator needs to be in the child so it doesn't teleport to 0 0 0
    {
        private static readonly int Attack = Animator.StringToHash("Attack");


        [Header("Combat")] public float attackCooldown = 5f;
        public float attackRange = 40f;
        public float activationRange = 30f;
        public float rotationSpeed = 5f;
        public GameObject player;

        [Header("Movement")] [Tooltip("Cooldown between moving action (when not in combat)")]
        public float moveCooldown = 15f;

        public float portalCooldown = 10f;

        [Tooltip("Distance from player to spawn portal")]
        public float playerDistance = 12.5f;

        public float maxDistanceFromPlayer = 40f;
        
        [Tooltip("Max distance the wizard can teleport via portal")]
        public float maxTeleportDistance = 25f;
        [Tooltip("Min distance the wizard can teleport via portal")]
        public float minTeleportDistance = 5f;
        [Tooltip("Min distance the wizard can move when not in combat")]
        public float minMoveDistance = 5f;
        [Tooltip("Max distance the wizard can move when not in combat")]
        public float maxMoveDistance = 10f;
        [Tooltip("Max distance the wizard can move when in combat, min is 0")]
        public float combatMoveDistance = 5f;
        
        [Header("Attack Prefabs")] public GameObject portal;

        // 6 projectiles in all directions
        public GameObject bigAttack;

        // 3 projectiles in target direction
        public GameObject smallAttack;

        // two projectiles above wizard
        public GameObject homingAttack;


        private float _lastActionTime;
        private float _lastShootTime;
        private Vector3 _spawnPosition;

        private NavMeshAgent _agent;
        private Animator _animator;

        private void Start()
        {
            _lastActionTime = Time.time - 100;
            _lastShootTime = Time.time - 100;
            _agent = GetComponent<NavMeshAgent>();
            _spawnPosition = transform.position;
            _animator = transform.GetComponentInChildren<WizardAnimationScript>().animator;
            if (player.IsUnityNull())
            {
                player = GameObject.FindWithTag("Player");
                Debug.Log("Player not assigned to " + gameObject.name + ", finding via tag");
            }
        }

        private void Update()
        {
            if (Time.time - _lastActionTime > portalCooldown && transform.name == "Wizard")
            {
                _lastActionTime = Time.time;
                StartCoroutine(nameof(TeleportToRandomLocation));
            }

            if (Time.time - _lastShootTime > attackCooldown && Time.time - _lastActionTime > attackCooldown &&
                IsInRange(player, attackRange))
            {
                Debug.Log("In range, attacking");
                AttackPlayer();
                _lastShootTime = Time.time;
            }
        }

        // wizard should teleport away from target if they get too close, second portal should also face the target
        private IEnumerator TeleportToRandomLocation()
        {
            gameObject.transform.LookAt(player.transform);

            // will need to balance between performance and looks
            if (Vector3.Distance(player.transform.position, transform.position) >
                playerDistance + maxTeleportDistance - 10)
                yield break;

            // wizard will need to face the target so the portal is facing the right direction
            var destination = RandomNavmeshLocation(maxTeleportDistance, minTeleportDistance);
            var telLocation = GetTeleportLocation(); //temp, all destination switched with tellocation
            var playerPos = player.transform.position;
            var destinationDirection = (playerPos - telLocation).normalized;
            var position = transform.position;
            var directionToPlayer = (playerPos - position).normalized;

            // using static duration for simplicity, needs to be changed if the portal duration changes
            Destroy(Instantiate(portal, telLocation, Quaternion.LookRotation(destinationDirection)), 5);
            yield return new WaitForSeconds(0.25f);
            // rotation switched with quaternion
            Destroy(Instantiate(portal, position, Quaternion.LookRotation(directionToPlayer)), 5);
            yield return new WaitForSeconds(1f);
            _agent.Warp(telLocation);
            yield return new WaitForSeconds(1f);
            MoveToRandomLocation();
        }

        private void MoveToRandomLocation(bool inCombat = false)
        {
            if (inCombat)
            {
                _agent.destination = RandomNavmeshLocation(combatMoveDistance, 0);
                return;
            }

            _agent.destination = RandomNavmeshLocation(maxMoveDistance, minMoveDistance);
        }

        private void AttackPlayer()
        {
            if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
                Debug.LogWarning("This shouldn't be called, " + gameObject.name + " is out of range");


            transform.LookAt(player.transform, transform.up);
            switch (Random.Range(0, 3))
            {
                case 0:
                    StartCoroutine(PerformAttack(smallAttack, 1f));
                    break;
                case 1:
                    StartCoroutine(PerformAttack(bigAttack, 1f));
                    break;
                case 2:
                    StartCoroutine(PerformAttack(homingAttack, 1f));
                    break;
            }
        }

        // animation needs to be played directly since it will otherwise be played after the method returns
        // because triggers are played the next frame, not immediately
        private IEnumerator PerformAttack(GameObject attackPrefab, float delay)
        {
            var wiz = transform;
            _animator.Play(Attack);
            yield return new WaitForSeconds(delay);
            wiz.LookAt(player.transform, wiz.up);
            Instantiate(attackPrefab, wiz.position, wiz.rotation);
        }


        // tries to find a location outside of player range but inside of teleport range
        // will return random location if it can't find a suitable one
        // but in that case the method shouldn't even be called
        private Vector3 GetTeleportLocation()
        {
            var destination = RandomNavmeshLocation(maxTeleportDistance, minTeleportDistance);
            var attempts = 0;

            if (Vector3.Distance(player.transform.position, transform.position) >
                playerDistance + maxTeleportDistance - 2)
                Debug.LogWarning("Player is very far away, method shouldn't be called");

            do
            {
                if (attempts > 100)
                {
                    Debug.LogWarning("Couldn't find a suitable teleport location, aborting");
                    break;
                }

                destination = RandomNavmeshLocation(maxTeleportDistance, minTeleportDistance);
                attempts++;
            } while (Vector3.Distance(destination, player.transform.position) < playerDistance ||
                     Vector3.Distance(destination, player.transform.position) > maxDistanceFromPlayer);

            return destination;
        }
    }
}