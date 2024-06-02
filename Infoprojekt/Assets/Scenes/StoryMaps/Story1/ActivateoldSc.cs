using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.StoryMaps.Story1
{
    public class ActivateOldSc : MonoBehaviour
    {
        public string scene;
        private Scene _joinedMaps;

        // Start is called before the first frame update
        private void Start()
        {
            //hier muss noch hin das er zu fisherslake teleportiert wird bitte
            Activate("JoinedMap");
            SceneManager.UnloadSceneAsync(scene);
        }

        public static void Activate(string sceneName)
        {
            // Hole die Szene anhand des Namens
            var sceneToDeactivate = SceneManager.GetSceneByName(sceneName);

            // �berpr�fe, ob die Szene geladen ist
            if (sceneToDeactivate.isLoaded)
            {
                // Iteriere �ber alle Root-GameObjects in der Szene und deaktiviere sie
                foreach (var go in sceneToDeactivate.GetRootGameObjects())
                    go.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Szene " + sceneName + " ist nicht geladen.");
            }
            GameObject.FindWithTag("Teleport").SetActive(false);
        }
    }
}