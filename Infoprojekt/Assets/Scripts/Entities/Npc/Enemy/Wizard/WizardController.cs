using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc.Enemy.Wizard
{
    public class WizardController : Npc
    // the animator needs to be in the child so it doesn't teleport to 0 0 0
    {
        [Header("Attack")] public float attackCooldown = 5f;
        public float attackRange = 40f;
        public float activationRange = 30f;
        public GameObject player;

        [Header("Movement")] [Tooltip("Cooldown between moving action (when not in combat)")]
        public float moveCooldown = 15f;

        public float portalCooldown = 3f;

        public float maxTeleportDistance = 15f;
        public float minTeleportDistance = 5f;
        public float minMoveDistance = 5f;
        public float maxMoveDistance = 10f;

        [Header("Attack Prefabs")] public GameObject portal;
        public GameObject bigAttack;
        public GameObject smallAttack;
        public GameObject homingAttack;

        private NavMeshAgent _agent;
        private Vector3 _spawnPosition;
        private Animator _animator;

        // I don't know if this is the smartest way to do it since you have to access the child but it cleans the scene hierarchy
        // but to change it you'd just have to change Start() 
        // private Transform _tempObjects;
        // temp objects are supposed to be stored in child of the wizard

        private float _lastActionTime;
        private float _lastShootTime;
        private static readonly int Attack = Animator.StringToHash("Attack");


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
                Debug.Log("player not assigned, finding via tag");
            }
        }

        private void Update()
        {
            if (Time.time - _lastActionTime > portalCooldown && transform.name == "Wizard")
            {
                _lastActionTime = Time.time;
                StartCoroutine(nameof(TeleportToRandomLocation));
            }

            if (Time.time - _lastShootTime > attackCooldown && IsPlayerInRange(15, player))
            {
                Debug.Log("RANGE SHOT");
                AttackPlayer();
                _lastShootTime = Time.time;
            }
        }

        // wizard should teleport away from target if they get too close, second portal should also face the target
        private IEnumerator TeleportToRandomLocation()
        {
            // this needs refactoring, wizard should almost always face the target, not just when teleporting
            // just keeping it for testing 
            gameObject.transform.LookAt(player.transform);


            // wizard will need to face the target so the portal is facing the right direction
            var destination = RandomNavmeshLocation(maxTeleportDistance, minTeleportDistance);
            var wizard = transform;
            var rotation = wizard.rotation;

            // using current duration for simplicity, needs to be changed if the portal duration changes
            Destroy(Instantiate(portal, destination, rotation), 5);
            yield return new WaitForSeconds(0.25f);
            Destroy(Instantiate(portal, wizard.position, rotation), 5);
            yield return new WaitForSeconds(1f);
            _agent.Warp(destination);
            MoveToRandomLocation();
        }

        private void MoveToRandomLocation()
        {
            _agent.SetDestination(RandomNavmeshLocation(maxMoveDistance, minMoveDistance));
        }

        // might add some more logic later
        private void AttackPlayer()
        {
            if (Vector3.Distance(transform.position, player.transform.position) > attackRange) return;
            transform.LookAt(player.transform, transform.up);
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                    LightAttack();
                    break;
                case 1:
                    HeavyAttack();
                    break;
                case 2:
                    HomingAttack();
                    break;
            }
        }

        // 3 projectiles in target direction
        private void LightAttack()
        {
            _animator.SetTrigger(Attack);
            var wiz = transform;
            Instantiate(smallAttack, wiz.position, wiz.rotation);
        }

        // 5 projectiles in all directions
        private void HeavyAttack()
        {
            _animator.SetTrigger(Attack);
            var wiz = transform;
            Instantiate(bigAttack, wiz.position, wiz.rotation);
        }

        // two projectiles above wizard
        private void HomingAttack()
        {
            _animator.SetTrigger(Attack);
            var wiz = transform;
            Instantiate(homingAttack, wiz.position, wiz.rotation);
        }
    }
}