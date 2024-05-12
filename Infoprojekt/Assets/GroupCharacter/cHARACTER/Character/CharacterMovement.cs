using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController charact;
    public GameObject cam;
    public bool haus = true;
    public PlayableDirector cutsceneDirector;

    public bool jumpPressed;
    public bool sneakPressed;
    public float walkpace;
    public float sprintpace;
    public bool attackdamage2;
    public bool attackdamage1;
    public bool attackdamage;
    public bool attackBowPressed;
    public bool rotein;
    public Transform characterMesh;
    public GameObject Arrow;
    public GameObject RefPoint;
    public float acceleration = 4;
    public float deceleration = 2;
    public bool forwardPressed;
    public bool leftPressed;
    public bool rightPressed;
    public float turnSmoothTime;
    public float rotationSpeed = 5f;
    public bool pickUpPressed;
    public bool rota;

    public GameObject sword;
    public GameObject bow;


    //BokAttack
    public int book1;
    public int book2;
    public int book3;
    public float price1;
    public float price2;
    public float price3;
    private Animator animator;
    private GameObject arr;
    private Arrow arrow;

    private float arrowTimer;

    //  CinemachineFreeLook cineCam;
    private bool attackPressed;

    private bool attackReleased;
    //Swords and Bows for activation and deacivation

    private bool backPressed;
    private Book book;
    private bool canRelease;
    private Vector2 currentMovement;
    private Vector2 CurrentMovementInput;
    private Quaternion currentrotation;
    private Vector3 CurrentRotation;
    private Vector2 CurrentRotationInput;
    private bool firstbook;
    private PlayerInput input;
    private bool isRotationPressed;
    private int isRunningHash;
    private int isWalkingHash;
    private readonly float maximumWalkVelocity = 0.5f;
    private readonly float maximuRunVelocity = 2.0f;
    private bool movementPressed;
    private PlayerAttack playerattack;
    private PlayerHealth plhe;
    private Rigidbody rb;
    private bool runPressed;
    private bool secondbook;
    private bool thirdbook;
    private float turnSmoothVelocity;
    private float velocity;
    private float velocityX;

    private float velocityZ;

    private void Awake()
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

    // Start is called before the first frame update
    private void Start()
    {
        playerattack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        plhe = GetComponent<PlayerHealth>();
        book = GetComponent<Book>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");


        //Bogen
        if (!attackBowPressed && canRelease) attackReleased = true;
        if (attackBowPressed)
        {
            sword.SetActive(false);
            bow.SetActive(true);
            arrowTimer += Time.deltaTime;
        }


        charact.transform.position = characterMesh.position;
        if (!plhe.dead)
        {
            var isShooting = animator.GetBool("shoot");
            var isRunning = animator.GetBool(isRunningHash);
            var isWalking = animator.GetBool(isWalkingHash);
            handleMovement();
            handleRotation();
            if (!plhe.dead)
            {
            }
        }


        Motion();
    }

    private void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        input.CharacterControls.Disable();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Collectable" && pickUpPressed)
        {
            Debug.Log("kurva");

            var script = other.gameObject.GetComponent<ItemPickUp>();
            script.EPressed = true;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "attack")
        {
            Debug.Log("getroofen");
            plhe.Health -= 5;
        }
        if(other.gameObject.tag == "storyone")
        {
            Debug.Log("story");

        }
    }


    private void onRotationInput(InputAction.CallbackContext context)
    {
        CurrentRotationInput = context.ReadValue<Vector2>();
        CurrentRotation.x = CurrentRotationInput.x;
        CurrentRotation.y = CurrentRotationInput.y;
        isRotationPressed = CurrentRotationInput.x != 0 || CurrentRotationInput.y != 0;
    }

    private void Motion()
    {
        var currentMaxVelocity = runPressed ? maximuRunVelocity : maximumWalkVelocity;
        if (forwardPressed && velocityZ < currentMaxVelocity) velocityZ += Time.deltaTime * acceleration;

        if (leftPressed && velocityX > -currentMaxVelocity) velocityX -= Time.deltaTime * acceleration;
        if (rightPressed && velocityX < currentMaxVelocity) velocityX += Time.deltaTime * acceleration;
        if (backPressed && velocityZ > -currentMaxVelocity) velocityZ -= Time.deltaTime * acceleration;
        if (!forwardPressed && velocityZ > 0.0f) velocityZ -= Time.deltaTime * deceleration;
        if (!forwardPressed && velocityZ > 0.0f) velocityZ -= Time.deltaTime * deceleration;
        if (!backPressed && velocityZ < 0.0f) velocityZ += Time.deltaTime * deceleration;
        if (!leftPressed && velocityX < 0.0f) velocityX += Time.deltaTime * deceleration;
        if (!rightPressed && velocityX > 0.0f) velocityX -= Time.deltaTime * deceleration;

        if (!leftPressed && !rightPressed && velocityX != 0.0f && velocityX > -0.05f && velocityX < 0.05f)
            velocityX = 0.0f;
        if (!forwardPressed && !backPressed && velocityZ != 0.0f && velocityZ > -0.05f && velocityZ < 0.05f)
            velocityZ = 0.0f;


        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < currentMaxVelocity + 0.05f)
                velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > currentMaxVelocity - 0.05f)
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
            if (velocityX > currentMaxVelocity && velocityX < currentMaxVelocity + 0.05f)
                velocityX = currentMaxVelocity;
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > currentMaxVelocity - 0.05f)
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
            if (velocityX < -currentMaxVelocity && velocityX > -currentMaxVelocity - 0.05f)
                velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < -currentMaxVelocity + 0.05f)
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
            if (velocityZ < -currentMaxVelocity && velocityZ > -currentMaxVelocity - 0.05f)
                velocityZ = -currentMaxVelocity;
        }
        else if (backPressed && velocityZ > -currentMaxVelocity && velocityZ < -currentMaxVelocity + 0.05f)
        {
            velocityZ = -currentMaxVelocity;
        }


        animator.SetFloat("VelocityZ", velocityZ);
        animator.SetFloat("VelocityX", velocityX);
    }

    private void handleRotation()
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


        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.transform.eulerAngles.y, ref turnSmoothVelocity,
            turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
    }

    private void handleMovement()
    {
        var isRunning = animator.GetBool(isRunningHash);
        var isWalking = animator.GetBool(isWalkingHash);
        var isShooting = animator.GetBool("shoot");
        var isBow = animator.GetBool("arrowStand");
        var wpressed = Input.GetKey(KeyCode.W);
        var isJumping = animator.GetBool("jump");
        var isSneaking = animator.GetBool("sneak");

        if (movementPressed && !isWalking)
        {
            haus = true;
            animator.SetBool(isWalkingHash, true);
            animator.SetBool("shoot", false);
        }

        if (!movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
            currentrotation = transform.rotation;
            haus = false;
        }

        if (movementPressed && runPressed && !isRunning)
        {
            haus = true;
            animator.SetBool(isRunningHash, true);
            animator.SetBool("shoot", false);
        }

        if ((!movementPressed || !runPressed) && isRunning) animator.SetBool(isRunningHash, false);
        if (attackPressed && !isShooting)
        {
            sword.SetActive(true);
            bow.SetActive(false);
            attackdamage1 = true;
            animator.SetBool("shoot", true);
        }

        if (!attackPressed) animator.SetBool("shoot", false);
        if (!attackPressed && !movementPressed)
        {
            //animator.SetBool("Idle",true);
        }
        else
        {
            animator.SetBool("Idle", false);
        }

        if (attackBowPressed && !isBow)
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

        if (!attackBowPressed) animator.SetBool("arrowStand", false);


        if (attackReleased)
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
            animator.SetBool("jump", true);
        else
            animator.SetBool("jump", false);

        //BookAttack
        if (firstbook && book.attackFinished && plhe.mana > price1)
        {
            book.attackFinished = false;
            plhe.mana -= price1;
            book.AttackBook(book1);
        }

        if (secondbook && book.attackFinished && plhe.mana > price2)
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
}