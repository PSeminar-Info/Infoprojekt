using UnityEngine;
using UnityEngine.Serialization;

namespace Terrain.See.Scripts
{
    public class ToggleManager : MonoBehaviour
    {
        public bool village;
        public bool forest;
        public bool graveyard;
        public bool castle;
        [FormerlySerializedAs("lazylake")] public bool lazyLake;
        [FormerlySerializedAs("fisherslake")] public bool fishersLake;
        [FormerlySerializedAs("tundracastle")] public bool tundraCastle;

        public void Changevillage(bool change)
        {
            village = change;
        }

        public void Changeforest(bool change)
        {
            forest = change;
        }

        public void Changegraveyard(bool change)
        {
            graveyard = change;
        }

        public void Changecastle(bool change)
        {
            castle = change;
        }

        public void Changelazylake(bool change)
        {
            lazyLake = change;
        }

        public void Changefisherslake(bool change)
        {
            fishersLake = change;
        }

        public void Changetundracastle(bool change)
        {
            tundraCastle = change;
        }

        public string GetMap()
        {
            return village ? "village" :
                forest ? "forest" :
                graveyard ? "graveyard" :
                castle ? "castle" :
                lazyLake ? "lazylake" :
                fishersLake ? "fisherslake" :
                tundraCastle ? "tundracastle" : "Error";
        }
    }
}