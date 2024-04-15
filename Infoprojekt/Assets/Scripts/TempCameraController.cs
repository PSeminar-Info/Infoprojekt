using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed = 1f;
    public Camera defaultCamera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // SetDefaultCamera();
    }

    private void Update()
    {
        var playerPos = player.position;
        var transformPos = transform.position;
        var viewDirection = playerPos - new Vector3(transformPos.x, playerPos.y, transformPos.z);

        orientation.forward = viewDirection.normalized;

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var inputDirection = orientation.forward * vertical + orientation.right * horizontal;

        if (inputDirection != Vector3.zero)
            playerObj.forward =
                Vector3.Slerp(playerObj.forward, inputDirection.normalized, Time.fixedDeltaTime * rotationSpeed);
    }

    private void SetDefaultCamera()
    {
        if (defaultCamera != null)
        {
            Camera.main.enabled = false; // Deaktiviere die vorherige Hauptkamera
            defaultCamera.enabled = true; // Aktiviere die neue Standardkamera
        }
        else
        {
            Debug.LogWarning("Keine Standardkamera zugewiesen!");
        }
    }
}