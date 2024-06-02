using UnityEngine;

namespace Entities.Npc.Enemy.Bear
{
    public class BearHitController : MonoBehaviour
    {
        private const float DespawnTime = 0.1f;
        private float _lastActionTime;

        private void Start()
        {
            _lastActionTime = Time.time;
        }

        private void Update()
        {
            if (Time.time - _lastActionTime > DespawnTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
