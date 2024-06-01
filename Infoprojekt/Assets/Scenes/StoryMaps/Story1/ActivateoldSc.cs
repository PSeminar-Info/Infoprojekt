using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateoldSc : MonoBehaviour
{
    Scene JoinedMaps;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("JoinedMap");
       //hier muss noch hin das er zu fisherslake teleportiert wird bitte

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
