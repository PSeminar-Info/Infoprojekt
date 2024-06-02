using UnityEngine;
using UnityEngine.Serialization;

namespace GroupCharacter.cHARACTER.Character.Book.Hovl_Studio.Magic_effects_pack.Prefabs.Magic_circles
{
    public class Healing : MonoBehaviour
    {
        [FormerlySerializedAs("HealAnimation")]
        public GameObject healAnimation;

        private GameObject _currentHealAnimation; // Speichert das aktuelle HealAnimation-Objekt

        private PlayerHealth _playerhealth;
        private float _timer;

        private void Start()
        {
            _timer = 0;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (_currentHealAnimation != null)
            {
                _currentHealAnimation.gameObject.transform.SetParent(null); // Oder: transform.parent = null;
                Destroy(_currentHealAnimation.gameObject);
            }

            _currentHealAnimation =
                Instantiate(healAnimation, collider.gameObject.transform.position, Quaternion.identity);
            _currentHealAnimation.gameObject.transform.SetParent(collider.gameObject.transform);
        }

        private void OnTriggerExit(Collider collider)
        {
            _timer = 0;
            if (_currentHealAnimation != null)
            {
                _currentHealAnimation.gameObject.transform.SetParent(null); // Oder: transform.parent = null;
                Destroy(_currentHealAnimation.gameObject);
            }
        }

        private void OnTriggerStay(Collider collider)
        {
            if (!collider.gameObject.CompareTag("Player")) return;
            _timer += Time.deltaTime;
            if (!(_timer >= 1)) return;
            _playerhealth = collider.GetComponent<PlayerHealth>();
            _playerhealth.health += 2;
            _timer = 0;
        }
    }
}