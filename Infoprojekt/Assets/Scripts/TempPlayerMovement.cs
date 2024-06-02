using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ctrl c ctrl v from https://www.youtube.com/watch?v=f473C43s8nE
    [Header("Movement")] public float moveSpeed = 5;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    public float walkSpeed;
    public float sprintSpeed;

    [Header("Keybinds")] public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")] public float playerHeight;
    public LayerMask whatIsGround;

    public Transform orientation;
    private bool _grounded;

    private float _horizontalInput;

    private Vector3 _moveDirection;
    private Rigidbody _rb;
    private bool _readyToJump;
    private float _verticalInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        _readyToJump = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        _grounded = true; //Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (_grounded)
            _rb.drag = groundDrag;
        else
            _rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        _rb.AddForce(Vector3.down * 100);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BearHit")) Debug.Log("Hit from bear!");
    }

    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKeyDown(jumpKey) && _readyToJump && _grounded)
        {
            _readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKey(KeyCode.LeftControl)) _rb.AddForce(Vector3.down * 1000, ForceMode.Force);
    }

    private void MovePlayer()
    {
        // calculate movement direction
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        switch (_grounded)
        {
            // on ground
            case true:
                _rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
                break;
            // in air
            case false:
                _rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
                break;
        }
    }

    private void SpeedControl()
    {
        var flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        // limit velocity if needed
        if (!(flatVel.magnitude > moveSpeed)) return;
        var limitedVel = flatVel.normalized * moveSpeed;
        _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
    }

    private void Jump()
    {
        // reset y velocity
        var velocity = _rb.velocity;
        velocity = new Vector3(velocity.x, 0f, velocity.z);
        _rb.velocity = velocity;

        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        _readyToJump = true;
    }
}