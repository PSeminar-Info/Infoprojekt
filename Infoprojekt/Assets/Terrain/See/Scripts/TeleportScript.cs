using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportScript : MonoBehaviour
{
    public GameObject Canvas;
    public ToggleManager togglemanager;

    public ToggleGroup ToggleGroup;
    private bool enter;

    private bool showGUI;

    //bool ob du schon das gebiet erkundet hast
    private bool[] unlocked = new bool[10];

    private void Start()
    {
        Canvas.SetActive(false);
    }

    private void Update()
    {
        //Opens Canvas if Player is in the Trigger Area & presses Key
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
        print(togglemanager.GetMap());
        if (togglemanager.GetMap().Equals("mountain")) SceneManager.LoadScene("Mountains");
        /*
        if (unlocked[1])
        {
            print("Super du wirst tpt");
        }
        else
        {
           StartCoroutine(Wait());
        }*/
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