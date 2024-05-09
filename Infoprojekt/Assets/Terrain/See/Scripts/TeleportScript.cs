using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportScript : MonoBehaviour
{
    public GameObject Canvas;
    public ToggleManager togglemanager;
    public MapDiscover mapdiscover;

    public ToggleGroup ToggleGroup;
    private bool enter;

    private bool showGUI;

    private void Start()
    {
        Canvas.SetActive(false);
    }

    private void Update()
    {
        //Öffnet des Canvas
        if (Input.GetKeyDown(KeyCode.F) && enter && Canvas != null)
        {
            Canvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
        }
    }

    private void OnGUI()
    {
        if (enter) GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 155, 30), "Press 'F' to teleport");

        //das geht sicher schöner juckt aber
        if (showGUI)
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 300, 250, 30),
                "Du hast dieses Gebiet noch nicht erkundet");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) enter = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) enter = false;
    }

    public void Teleport()
    {
        if (togglemanager.GetMap().Equals("lazylake"))
        {
            SceneManager.LoadScene("LazyLake");
            //Player.transform.position = new Vector3(2,-1,0);
        } 
        else if (togglemanager.GetMap().Equals("fisherslake"))
        {
            SceneManager.LoadScene("LakeTundra");
            //GameObject.FindWithTag("Player").transform.position = new Vector3(220, 34, 205);
        }
        else if(togglemanager.GetMap().Equals("village"))
        {
            SceneManager.LoadScene("StartMap");
            //GameObject.FindWithTag("Player").transform.position = new Vector3(860, 70, -150);
        }
        else if(togglemanager.GetMap().Equals("tundracastle"))
        {
            SceneManager.LoadScene("LakeTundra");
        }
        else if (togglemanager.GetMap().Equals("forest"))
        {
            SceneManager.LoadScene("StartMap");
        }
        else if (togglemanager.GetMap().Equals("graveyard"))
        {
            SceneManager.LoadScene("StartMap");
        }
        else if (togglemanager.GetMap().Equals("castle"))
        {
            SceneManager.LoadScene("StartMap");
        }
    }


    public void Cancel()
    {
        Canvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    //ja leck eier ich weiß der code ist scuffed
    private IEnumerator Wait()
    {
        //zeigt dass man des Gebiet noch nicht unlocked hat
        showGUI = true;
        yield return new WaitForSeconds(5);
        showGUI = false;
    }
}