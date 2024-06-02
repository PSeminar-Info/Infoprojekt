using UnityEngine;
using UnityEngine.Serialization;

namespace GroupCharacter.cHARACTER.Character.Book
{
    public class Book : MonoBehaviour
    {
        // Start is called before the first frame update
        public bool attackFinished;
        public GameObject book;
        public GameObject bookPoint;
        public Transform parent;
        [FormerlySerializedAs("Animation1")] public GameObject animation1;
        [FormerlySerializedAs("Animation2")] public GameObject animation2;
        [FormerlySerializedAs("Animation3")] public GameObject animation3;
        public bool playerEffect;
        private float _cooldown;
        private GeneralEffectScript _geneff;
        private float _spawnDistance;

        private void Start()
        {
            _spawnDistance = 10;
            attackFinished = true;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!attackFinished) _cooldown += Time.deltaTime;
            if (_cooldown > 2)
            {
                attackFinished = true;
                _cooldown = 0;
            }
        }

        public void AttackBook(int number)
        {
            switch (number)
            {
                case 1:
                    //Debug.Log("Fall 1 wurde ausgelöst.");
                    // Füge hier die Aktionen für Fall 1 hinzu
                    _geneff = animation1.GetComponent<GeneralEffectScript>();
                    _spawnDistance = _geneff.distance;
                    Ins(_spawnDistance, animation1);
                    break;
                case 2:
                    //Debug.Log("Fall 2 wurde ausgelöst.");

                    // Füge hier die Aktionen für Fall 2 hinzu
                    _geneff = animation2.GetComponent<GeneralEffectScript>();
                    _spawnDistance = _geneff.distance;
                    Ins(_spawnDistance, animation2);
                    break;
                case 3:
                    //Debug.Log("Fall 3 wurde ausgelöst.");

                    // Füge hier die Aktionen für Fall 3 hinzu
                    _geneff = animation3.GetComponent<GeneralEffectScript>();
                    _spawnDistance = _geneff.distance;
                    Ins(_spawnDistance, animation3);
                    break;
            }
        }

        public void Ins(float dis, GameObject a)
        {
            Instantiate(book, bookPoint.transform.position, Quaternion.identity);
            // Position des Spielers erhalten
            var playerPosition = transform.position;

            // Richtung vom Spieler zum Spawnpunkt berechnen
            var spawnDirection = transform.forward;

            // Berechnen der Spawnposition um spawnDistance vor dem Spieler
            var positiontospawn = playerPosition + spawnDirection * dis;

            Instantiate(a, positiontospawn, Quaternion.identity);
        }
    }
}