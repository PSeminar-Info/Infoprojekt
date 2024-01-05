using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MotionController : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2;
    public float deceleration = 2;
    bool forwardPressed;
    bool leftPressed;
    bool rightPressed;
    private Vector2 CurrentMovementInput;
    private bool runPressed = false;
    PlayerInput input;
    private bool attackPressed;

    // Start is called before the first frame update
    void Awake()
    {
        input = new PlayerInput();
        animator = this.GetComponent<Animator>();
        input.CharacterControls.Attack.performed += ctx => attackPressed = ctx.ReadValueAsButton();

    }
    void Start()
    {
       

    }
    void OnMovementInput(InputAction.CallbackContext context)
    {
        CurrentMovementInput = context.ReadValue<Vector2>();
        if (CurrentMovementInput.x > 0)
        {
            forwardPressed = true;
        }
        else
        {
            forwardPressed = false;
        }
        if (CurrentMovementInput.y < 0)
        {
            leftPressed = true;
        }
        else
        {
            rightPressed = false;
        }
        Debug.Log("g" + CurrentMovementInput);
    }
    // Update is called once per frame
    void Update()
    {
        Motion();
       if(attackPressed)
        {
            Debug.Log("grrrr");
        }
        Motion();
    }
    void Motion()
    {
        if(forwardPressed && velocityZ <0.5f && !runPressed)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
       
        if(leftPressed && velocityX > -0.5f && !runPressed)
        {
            velocityX -= Time.deltaTime * acceleration;

        }
        if (rightPressed && velocityX < 0.5f && !runPressed)
        {
            velocityX += Time.deltaTime * acceleration;

        }
        if(!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        if (!forwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0;
        }
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        if(!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }
        animator.SetFloat("VelocityZ", velocityZ);
        animator.SetFloat("VelocityX", velocityX);

    }
}
