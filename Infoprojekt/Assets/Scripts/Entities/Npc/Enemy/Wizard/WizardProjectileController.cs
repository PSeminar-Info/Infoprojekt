using UnityEngine;

namespace Entities.Npc.Enemy.Wizard
{
    public class WizardProjectileController : MonoBehaviour
    {
        public GameObject explosion;

        [Tooltip("Only needed for homing projectiles")]
        public GameObject target;

        [Tooltip("Force applied to projectile. Only used if Homing is false")]
        public float force = 10;

        [Header("Homing")] [Tooltip("Whether the projectile should home in on the target")]
        public bool isHoming;

        [Range(0.0f, 1.0f)] public float rotationSpeed = 1f;

        [Tooltip("Controls the speed of homing projectiles. Normal projectiles use force.")]
        public float speed = 10f;

        public float despawnDistance = 75f;


        private Rigidbody _rb;
        private Vector3 _spawnPosition;

        private void Start()
        {
            if (target == null)
                target = GameObject.FindGameObjectWithTag("Player");
            if (Vector3.Distance(transform.position, target.transform.position) > 100)
                Debug.Log("Distance between projectile and target is very big, this shouldn't occur");

            if (rotationSpeed < 0)
                rotationSpeed = 0;
            if (rotationSpeed > 1) rotationSpeed = 1;
            _rb = GetComponent<Rigidbody>();
            _spawnPosition = transform.position;
        }

        private void FixedUpdate()
        {
            if (isHoming)
            {
                var trans = transform;
                var targetPos = target.transform.position;
                // offset by .5 so it doesn't hit the ground
                var targetPosition = new Vector3(targetPos.x, targetPos.y + 0.5f, targetPos.z);
                var direction = (targetPosition - trans.position).normalized;
                var rotateDir = Vector3.RotateTowards(trans.forward, direction, rotationSpeed * Time.deltaTime, 0.0f);
                var rotation = Quaternion.LookRotation(direction);

                trans.rotation = Quaternion.LookRotation(rotateDir);
                _rb.MoveRotation(Quaternion.RotateTowards(trans.rotation, rotation,
                    rotationSpeed * Time.deltaTime));
                _rb.velocity = trans.forward * speed;
                return;
            }

            _rb.AddForce(transform.forward * force);
            if (Vector3.Distance(transform.position, _spawnPosition) > despawnDistance)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 3);
            Destroy(gameObject);
        }
    }
}