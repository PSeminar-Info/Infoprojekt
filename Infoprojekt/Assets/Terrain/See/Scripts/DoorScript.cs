using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Smoothly open a door
    public AnimationCurve openSpeedCurve =
        new(new Keyframe(0, 1, 0, 0), new Keyframe(0.8f, 1, 0, 0),
            new Keyframe(1, 0, 0,
                0)); //Contols the open speed at a specific time (ex. the door opens fast at the start then slows down at the end)

    public float openSpeedMultiplier = 2.0f; //Increasing this value will make the door open faster
    public float doorOpenAngle = 90.0f; //Global door open speed that will multiply the openSpeedCurve
    private float currentRotationAngle;

    private float defaultRotationAngle;
    private bool enter;


    private bool open;
    private float openTime;

    private void Start()
    {
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;

        //Set Collider as trigger
        //GetComponent<Collider>().isTrigger = true;
    }

    // Main function
    private void Update()
    {
        if (openTime < 1) openTime += Time.deltaTime * openSpeedMultiplier * openSpeedCurve.Evaluate(openTime);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
            Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime),
            transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.E) && enter)
        {
            open = !open;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
        }
    }

    // Display a simple info message when player is inside the trigger area (This is for testing purposes only so you can remove it)
    private void OnGUI()
    {
        if (enter)
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 155, 30),
                "Press 'E' to " + (open ? "close" : "open") + " the door");
    }

    //
    // Activate the Main function when Player enter the trigger area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) enter = true;
    }

    // Deactivate the Main function when Player exit the trigger area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) enter = false;
    }
}