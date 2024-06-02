using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.StoryMaps.Story1
{
    public class ActivateoldSc : MonoBehaviour
    {
        private Scene _joinedMaps;

        // Start is called before the first frame update
        void Start()
        {
            SceneManager.LoadScene("JoinedMap");
            //hier muss noch hin das er zu fisherslake teleportiert wird bitte

        }
    }
}
