using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateoldSc : MonoBehaviour
{
    Scene JoinedMaps;
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        //hier muss noch hin das er zu fisherslake teleportiert wird bitte
        Activate("JoinedMap");
        SceneManager.UnloadSceneAsync(scene);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate(string sceneName)
    {
        // Hole die Szene anhand des Namens
        Scene sceneToDeactivate = SceneManager.GetSceneByName(sceneName);

        // Überprüfe, ob die Szene geladen ist
        if (sceneToDeactivate.isLoaded)
        {
            // Iteriere über alle Root-GameObjects in der Szene und deaktiviere sie
            foreach (GameObject go in sceneToDeactivate.GetRootGameObjects())
            {
                go.SetActive(true);
            }
            GameObject.FindWithTag("Teleport").SetActive(false);
        }
        else
        {
            Debug.LogWarning("Szene " + sceneName + " ist nicht geladen.");
        }
    }
}
