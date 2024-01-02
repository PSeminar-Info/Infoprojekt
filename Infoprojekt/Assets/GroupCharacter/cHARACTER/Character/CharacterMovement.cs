using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    public CharacterController charact;
    PlayerInput input;
    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;
    Quaternion currentrotation;
    public GameObject cam;
    private float velocity;
    public bool haus = true;
    Vector2 CurrentRotationInput;
    Vector3 CurrentRotation;
    bool isRotationPressed;
    public  CinemachineFreeLook cineCam;
    private bool attackPressed = false;
    
    public bool jumpPressed;
    PlayerHealth plhe;
    public bool sneakPressed;
    public float walkpace;
    public float sprintpace;
    PlayerAttack playerattack;
    public bool attackdamage2;
    public bool attackdamage1;
    public bool attackdamage;
    public bool attackArrowPressed;
    public bool rotein = false;
    public Transform characterMesh;
    public GameObject Arrow;
    Arrow arrow;
    private bool attackReleased;
    private bool canRelease;
    private float arrowTimer;
    GameObject arr;
    public GameObject RefPoint;
    private Rigidbody rb;

    void Awake()
    {
        walkpace = 3;
        sprintpace = 7;
        input = new PlayerInput();

        input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };
        input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        input.CharacterControls.Attack.performed += ctx => attackPressed = ctx.ReadValueAsButton();
        input.CharacterControls.Jump.performed += ctx => jumpPressed = ctx.ReadValueAsButton();
        input.CharacterControls.Sneak.performed += ctx => sneakPressed = ctx.ReadValueAsButton();
        input.CharacterControls.Attack.canceled += ctx => attackArrowPressed = ctx.ReadValueAsButton();


        input.CharacterControls.Rotate.started += onRotationInput;
        input.CharacterControls.Rotate.performed += onRotationInput;
        input.CharacterControls.Rotate.canceled += onRotationInput;

    }
    void onRotationInput(InputAction.CallbackContext context)
    {
        CurrentRotationInput = context.ReadValue<Vector2>();
        CurrentRotation.x = CurrentRotationInput.x;
        CurrentRotation.y = CurrentRotationInput.y;
        isRotationPressed = CurrentRotationInput.x != 0 || CurrentRotationInput.y != 0;

    }
    // Start is called before the first frame update
    void Start()
    {
        playerattack = this.GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        plhe = this.GetComponent<PlayerHealth>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        //Bogen
        if(!attackPressed && canRelease) 
        {
            attackReleased = true;
        }
        if(attackPressed)
        {
            arrowTimer += Time.deltaTime;

        }
       
        
        charact.transform.position = characterMesh.position;
        if (!plhe.dead)
        {
            bool isShooting = animator.GetBool("shoot");
            bool isRunning = animator.GetBool(isRunningHash);
            bool isWalking = animator.GetBool(isWalkingHash);
            handleMovement();
            handleRotation();
            if (!plhe.dead)
            {
                
            }
        }
        
            
        
       
    }
    void handleRotation()
    {
        if(haus)
        {
            currentrotation = cam.transform.rotation;

        }
        
            var targetangle = Mathf.Atan2(currentMovement.x, currentMovement.y) * Mathf.Rad2Deg;
            transform.rotation = currentrotation * Quaternion.Euler(0, targetangle, 0);
            var angleright = Mathf.Atan2(CurrentRotation.x, CurrentRotation.y) * Mathf.Rad2Deg;
            var angler = currentrotation * Quaternion.Euler(0, angleright, 0);
            cineCam.m_XAxis.Value = CurrentRotation.x * 200 * Time.deltaTime;
            cineCam.m_YAxis.Value += CurrentRotation.y * -5 * Time.deltaTime;
       
        

    }
    void handleMovement()
    {

        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isShooting = animator.GetBool("shoot");
        bool wpressed = Input.GetKey(KeyCode.W);
        bool isJumping = animator.GetBool("jump");
        bool isSneaking = animator.GetBool("sneak");
       
        if ((movementPressed) && !isWalking)
        {
            haus = true;
            animator.SetBool(isWalkingHash, true);
            animator.SetBool("shoot", false);
           
        }
        if (!movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
            currentrotation = this.transform.rotation;
            haus = false;
        }
        if ((movementPressed && runPressed) && !isRunning)
        {
            haus = true;
            animator.SetBool(isRunningHash, true);
            animator.SetBool("shoot", false);
        }
        if ((!movementPressed || !runPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
            
           
        }
        if (attackPressed && !isShooting)
        {
            attackdamage1 = true;
            animator.SetBool("shoot", true);
            canRelease = true;
            arr = Instantiate(Arrow, RefPoint.transform.position, RefPoint.transform.rotation);
            arr.transform.Rotate(new Vector3(0, 0, 90));
            arr.transform.parent = RefPoint.transform;
            rb = arr.GetComponent<Rigidbody>();
            rb.isKinematic = true;

        }
        if (!attackPressed)
        {
            animator.SetBool("shoot", false);

        }
        if (!attackPressed && !movementPressed)
        {
            animator.SetBool("Idle",true);
        }
        else
        {
            animator.SetBool("Idle", false);


        }
        if(attackReleased)
        {

            rb.isKinematic = false;

            Debug.Log("geht" + arrowTimer);
            arr.transform.parent = null;

            arrow = arr.GetComponent<Arrow>();

            arrow.Shoot(arrowTimer * 40);
            attackReleased = false;
            canRelease = false;
            arrowTimer = 0;
        }




        if (jumpPressed)
         { 
        
            animator.SetBool("jump", true);
         }
        else
        {
            animator.SetBool("jump", false);
        }
        



    }
    void OnEnable()
    {
        input.CharacterControls.Enable();
    }
    void OnDisable()
    {
        input.CharacterControls.Disable();
    }
    
}
