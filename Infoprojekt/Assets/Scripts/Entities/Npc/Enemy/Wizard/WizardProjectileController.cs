using UnityEngine;

namespace Entities.Npc.Enemy.Wizard
{
    public class WizardProjectileController : MonoBehaviour
    {
        public GameObject explosion;

        [Tooltip("Only needed for homing projectiles")]
        public GameObject player;

        [Tooltip("Whether the projectile should home in on the player")]
        public bool isHoming;

        [Tooltip(
            "[0-1] Only used if Homing is true. Controls how fast the projectile should rotate towards the player")]
        public float rotationSpeed = 0.05f;

        private Rigidbody _rigidBody;
        private GameObject _parent;

        private void Start()
        {
            if (rotationSpeed < 0) rotationSpeed = 0;
            if (rotationSpeed > 1) rotationSpeed = 1;

            _rigidBody = GetComponent<Rigidbody>();
            _parent = gameObject.transform.parent.gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 3);
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            // should work, but need player to test
            // if (isHoming)
            //     transform.rotation = Quaternion.Slerp(transform.rotation, _parent.transform.rotation, rotationSpeed);

            _rigidBody.AddForce(transform.forward * 10);
            if (Vector3.Distance(transform.position, _parent.transform.position) > 50)
                Destroy(gameObject);
        }
    }
}