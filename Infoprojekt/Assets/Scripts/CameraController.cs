using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed = 1f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
                Vector3.Slerp(playerObj.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
    }
}