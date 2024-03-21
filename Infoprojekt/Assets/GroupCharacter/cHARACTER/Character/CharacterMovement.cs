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
    //  CinemachineFreeLook cineCam;
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
    public bool attackBowPressed;
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

    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 4;
    public float deceleration = 2;
    float maximumWalkVelocity = 0.5f;
    float maximuRunVelocity = 2.0f;
    public bool forwardPressed;
    public bool leftPressed;
    public bool rightPressed;
    private Vector2 CurrentMovementInput;
    //Swords and Bows for activation and deacivation
   
    bool backPressed;
    float turnSmoothVelocity;
    public float turnSmoothTime;
    public float rotationSpeed = 5f;
    public bool pickUpPressed;
    public bool rota;
    


    //BokAttack
    public int book1;
    public int book2;
    public int book3;
    Book book;
    private bool firstbook;
    private bool secondbook;
    private bool thirdbook;
    public float price1;
    public float price2;
    public float price3;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        walkpace = 3;
        sprintpace = 7;
        input = new PlayerInput();

       
        input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        input.CharacterControls.Attack.performed += ctx => attackPressed = ctx.ReadValueAsButton();
        input.CharacterControls.Jump.performed += ctx => jumpPressed = ctx.ReadValueAsButton();
        input.CharacterControls.Sneak.performed += ctx => sneakPressed = ctx.ReadValueAsButton();
        input.CharacterControls.AttackBow.performed += ctx => attackBowPressed = ctx.ReadValueAsButton();
        input.CharacterControls.PickUp.performed += ctx => pickUpPressed = ctx.ReadValueAsButton();

        input.CharacterControls.BookFirst.performed += ctx => firstbook = ctx.ReadValueAsButton();
        input.CharacterControls.BookSecond.performed += ctx => secondbook = ctx.ReadValueAsButton();
        input.CharacterControls.BookThird.performed += ctx => thirdbook = ctx.ReadValueAsButton();


        input.CharacterControls.Rotate.started += onRotationInput;
        input.CharacterControls.Rotate.performed += onRotationInput;
        input.CharacterControls.Rotate.canceled += onRotationInput;

        input.CharacterControls.W.performed += ctx => forwardPressed = ctx.ReadValueAsButton();
        input.CharacterControls.A.performed += ctx => leftPressed = ctx.ReadValueAsButton();
        input.CharacterControls.S.performed += ctx => backPressed = ctx.ReadValueAsButton();
        input.CharacterControls.D.performed += ctx => rightPressed = ctx.ReadValueAsButton();


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
        book = this.GetComponent<Book>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");


       
        //Bogen
        if(!attackBowPressed && canRelease) 
        {
            attackReleased = true;
        }
        if(attackBowPressed)
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


        Motion();
       
    }
   
    void Motion()
    {
        
        
        float currentMaxVelocity = runPressed ? maximuRunVelocity : maximumWalkVelocity;
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;

        }
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;

        }
        if (backPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ  -= Time.deltaTime * deceleration;
        }
        if (!backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }
        if (!forwardPressed && !backPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }



        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
            
        }
        else if(forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if(forwardPressed && velocityZ < currentMaxVelocity && velocityZ >(currentMaxVelocity-0.05f))
        {
            velocityZ = currentMaxVelocity;
        }




        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;

        }
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }



        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;

        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX <- currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        else if (leftPressed && velocityX >- currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }




        if (backPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;

        }
        else if (backPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05f))
            {
                velocityZ = -currentMaxVelocity;
            }
        }
        else if (backPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
        {
            velocityZ = -currentMaxVelocity;
        }





        animator.SetFloat("VelocityZ", velocityZ);
        animator.SetFloat("VelocityX", velocityX);

    }
    void handleRotation()
    {
        // if(haus)
        // {
        //    currentrotation = cam.transform.rotation;

        // }

        //     var targetangle = Mathf.Atan2(currentMovement.x, currentMovement.y) * Mathf.Rad2Deg;
        //     transform.rotation = currentrotation * Quaternion.Euler(0, targetangle, 0);
        //    var angleright = Mathf.Atan2(CurrentRotation.x, CurrentRotation.y) * Mathf.Rad2Deg;
        //     var angler = currentrotation * Quaternion.Euler(0, angleright, 0);
        // cineCam.m_XAxis.Value = CurrentRotation.x * 200 * Time.deltaTime;
        //  cineCam.m_YAxis.Value += CurrentRotation.y * -5 * Time.deltaTime;


        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.transform.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);


    }
    void handleMovement()
    {

        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isShooting = animator.GetBool("shoot");
        bool isBow = animator.GetBool("arrowStand");
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
            

        }
        if (!attackPressed)
        {
            animator.SetBool("shoot", false);

        }
        if (!attackPressed && !movementPressed)
        {
            //animator.SetBool("Idle",true);
        }
        else
        {
            animator.SetBool("Idle", false);


        }

        if(attackBowPressed && !isBow)
        {
           // AimCam.gameObject.SetActive(true);

           // ThirdPersonCam.gameObject.SetActive(false);
            // AimCam.m_XAxis.Value = ThirdPersonCam.m_XAxis.Value;
            // AimCam.m_YAxis.Value = ThirdPersonCam.m_YAxis.Value;
           
            animator.SetBool("arrowStand", true);
            canRelease = true;
            arr = Instantiate(Arrow, RefPoint.transform.position, RefPoint.transform.rotation);
            arr.transform.Rotate(new Vector3(0, 0, 90));
            arr.transform.parent = RefPoint.transform;
            rb = arr.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
        if(!attackBowPressed)
        {
            
            animator.SetBool("arrowStand", false);
            
        }


        if(attackReleased)
        {

            //ThirdPersonCam.gameObject.SetActive(true);
            //AimCam.gameObject.SetActive(false);
          //  ThirdPersonCam.VerticalAxis.Value = AimCam.m_VerticalAxis.Value;
            //ThirdPersonCam.m_YAxis.Value = AimCam.m_YAxis.Value;
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

        //BookAttack
        if(firstbook && book.attackFinished && plhe.mana > price1)
        {
            book.attackFinished = false;
            plhe.mana -= price1;
            book.AttackBook(book1);
        }
        if(secondbook && book.attackFinished && plhe.mana > price2)
        {
            book.attackFinished = false;
            plhe.mana -= price2;
            book.AttackBook(book2);

        }
        if (thirdbook && book.attackFinished && plhe.mana > price3)
        {
            book.attackFinished = false;
            plhe.mana -= price3;
            book.AttackBook(book3);

        }




    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Collectable" && pickUpPressed)
        {
            Debug.Log("kurva");

            ItemPickUp script = other.gameObject.GetComponent<ItemPickUp>();
            script.EPressed = true;
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
