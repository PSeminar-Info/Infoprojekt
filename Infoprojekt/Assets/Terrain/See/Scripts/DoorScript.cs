using UnityEngine;

namespace Terrain.See.Scripts
{
    public class DoorScript : MonoBehaviour
    {
        // Smoothly open a door
        public AnimationCurve openSpeedCurve =
            new(new Keyframe(0, 1, 0, 0), new Keyframe(0.8f, 1, 0, 0),
                new Keyframe(1, 0, 0,
                    0)); //Controls the open speed at a specific time (ex. the door opens fast at the start then slows down at the end)

        public float openSpeedMultiplier = 2.0f; //Increasing this value will make the door open faster
        public float doorOpenAngle = 90.0f; //Global door open speed that will multiply the openSpeedCurve
        private float _currentRotationAngle;

        private float _defaultRotationAngle;
        private bool _enter;


        private bool _open;
        private float _openTime;

        private void Start()
        {
            _defaultRotationAngle = transform.localEulerAngles.y;
            _currentRotationAngle = transform.localEulerAngles.y;

            //Set Collider as trigger
            //GetComponent<Collider>().isTrigger = true;
        }

        // Main function
        private void Update()
        {
            if (_openTime < 1) _openTime += Time.deltaTime * openSpeedMultiplier * openSpeedCurve.Evaluate(_openTime);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                Mathf.LerpAngle(_currentRotationAngle, _defaultRotationAngle + (_open ? doorOpenAngle : 0), _openTime),
                transform.localEulerAngles.z);

            if (Input.GetKeyDown(KeyCode.E) && _enter)
            {
                _open = !_open;
                _currentRotationAngle = transform.localEulerAngles.y;
                _openTime = 0;
            }
        }

        // Display a simple info message when player is inside the trigger area (This is for testing purposes only so you can remove it)
        private void OnGUI()
        {
            if (_enter)
                GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 155, 30),
                    "Press 'E' to " + (_open ? "close" : "open") + " the door");
        }

        //
        // Activate the Main function when Player enter the trigger area
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) _enter = true;
        }

        // Deactivate the Main function when Player exit the trigger area
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) _enter = false;
        }
    }
}