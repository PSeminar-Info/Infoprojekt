using Entities.Npc.Enemy.Bear;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Entities.Hostile
{
    public class SpawnBear : MonoBehaviour
    {
        [FormerlySerializedAs("Bear")] public GameObject bear;
        public Text info;
        private BearController _bearController;
        private bool _retu;

        public void Start()
        {
            _bearController = bear.GetComponent<BearController>();
        }

        public void Update()
        {
            if (_bearController.isDead &&
                _retu == false) // also check that this only executes once beacuse otherwise you could change text in the future
            {
                info.text = "Suche den Friedhof";
                _retu = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) bear.SetActive(true);
        }
    }
}