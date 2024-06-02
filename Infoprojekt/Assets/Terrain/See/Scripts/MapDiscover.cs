using UnityEngine;
using UnityEngine.Serialization;

namespace Terrain.See.Scripts
{
    public class MapDiscover : MonoBehaviour
    {
        public bool forest;
        public bool graveyard;
        public bool castle;
        [FormerlySerializedAs("lazylake")] public bool lazyLake;

        private void OnTriggerEnter(Collider other)
        {
            //checkt den Tag des Colliders und setzt dann die discover bool auf true
            switch (tag)
            {
                case "forest":
                    forest = true;
                    break;
                case "graveyard":
                    graveyard = true;
                    break;
                case "castle":
                    castle = true;
                    break;
            }
        }

        public bool ReturnDiscover(string map)
        {
            return map switch
            {
                "forest" => forest,
                "graveyard" => graveyard,
                "castle" => castle,
                _ => false
            };
        }
    }
}