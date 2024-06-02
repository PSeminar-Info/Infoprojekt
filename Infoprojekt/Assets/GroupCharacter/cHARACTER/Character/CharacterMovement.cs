using GroupCharacter.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GroupCharacter.cHARACTER.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        public CharacterController charact;
        public GameObject cam;
        public bool haus = true;
        public string sceneName;
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
        [FormerlySerializedAs("Arrow")] public GameObject arrow;
        [FormerlySerializedAs("RefPoint")] public GameObject refPoint;
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
        private Animator _animator;
        private GameObject _arr;
        private Arrow _arrow;

        private float _arrowTimer;

        //  CinemachineFreeLook cineCam;
        private bool _attackPressed;

        private bool _attackReleased;
        //Swords and Bows for activation and deacivation

        private bool _backPressed;
        private Book.Book _book;
        private bool _canRelease;
        private Vector2 _currentMovement;
        private Vector2 _currentMovementInput;
        private Quaternion _currentrotation;
        private Vector3 _currentRotation;
        private Vector2 _currentRotationInput;
        private bool _firstbook;
        private PlayerInput _input;
        private bool _isRotationPressed;
        private int _isRunningHash;
        private int _isWalkingHash;
        private const float MaximumWalkVelocity = 0.5f;
        private const float MaximumRunVelocity = 2.0f;
        private bool _movementPressed;
        private PlayerAttack _playerattack;
        private PlayerHealth _plhe;
        private Rigidbody _rb;
        private bool _runPressed;
        private bool _secondbook;
        private bool _thirdbook;
        private float _turnSmoothVelocity;
        private float _velocity;
        private float _velocityX;

        private bool _opendings;
        public GameObject panelDings;
        [FormerlySerializedAs("PanelNormal")] public GameObject panelNormal;

        private float _velocityZ;
        private static readonly int Jump = Animator.StringToHash("jump");
        private static readonly int ArrowStand = Animator.StringToHash("arrowStand");
        private static readonly int Shoot = Animator.StringToHash("shoot");
        private static readonly int Sneak = Animator.StringToHash("sneak");
        private static readonly int Idle = Animator.StringToHash("Idle");

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            walkpace = 3;
            sprintpace = 7;
            _input = new PlayerInput();


            _input.CharacterControls.Run.performed += ctx => _runPressed = ctx.ReadValueAsButton();
            _input.CharacterControls.Attack.performed += ctx => _attackPressed = ctx.ReadValueAsButton();
            _input.CharacterControls.Jump.performed += ctx => jumpPressed = ctx.ReadValueAsButton();
            _input.CharacterControls.Sneak.performed += ctx => sneakPressed = ctx.ReadValueAsButton();
            _input.CharacterControls.AttackBow.performed += ctx => attackBowPressed = ctx.ReadValueAsButton();
            _input.CharacterControls.PickUp.performed += ctx => pickUpPressed = ctx.ReadValueAsButton();

            _input.CharacterControls.BookFirst.performed += ctx => _firstbook = ctx.ReadValueAsButton();
            _input.CharacterControls.BookSecond.performed += ctx => _secondbook = ctx.ReadValueAsButton();
            _input.CharacterControls.BookThird.performed += ctx => _thirdbook = ctx.ReadValueAsButton();

            _input.CharacterControls.OpenDings.performed += ctx => _opendings = ctx.ReadValueAsButton();


            _input.CharacterControls.Rotate.started += OnRotationInput;
            _input.CharacterControls.Rotate.performed += OnRotationInput;
            _input.CharacterControls.Rotate.canceled += OnRotationInput;

            _input.CharacterControls.W.performed += ctx => forwardPressed = ctx.ReadValueAsButton();
            _input.CharacterControls.A.performed += ctx => leftPressed = ctx.ReadValueAsButton();
            _input.CharacterControls.S.performed += ctx => _backPressed = ctx.ReadValueAsButton();
            _input.CharacterControls.D.performed += ctx => rightPressed = ctx.ReadValueAsButton();
        }

        // Start is called before the first frame update
        private void Start()
        {
            _playerattack = GetComponent<PlayerAttack>();
            _animator = GetComponent<Animator>();
            _plhe = GetComponent<PlayerHealth>();
            _book = GetComponent<Book.Book>();
            _isWalkingHash = Animator.StringToHash("isWalking");
            _isRunningHash = Animator.StringToHash("isRunning");
        }

        // Update is called once per frame
        private void Update()
        {
            if (_opendings)
            {

                panelDings.SetActive(true);
                panelNormal.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;




            }
            if (panelNormal.active)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }

            var horizontalInput = Input.GetAxis("Horizontal");


            //Bogen
            if (!attackBowPressed && _canRelease) _attackReleased = true;
            if (attackBowPressed)
            {
                sword.SetActive(false);
                bow.SetActive(true);
                _arrowTimer += Time.deltaTime;
            }


            charact.transform.position = characterMesh.position;
            if (!_plhe.dead)
            {
                var isShooting = _animator.GetBool(Shoot);
                var isRunning = _animator.GetBool(_isRunningHash);
                var isWalking = _animator.GetBool(_isWalkingHash);
                HandleMovement();
                HandleRotation();
                if (!_plhe.dead)
                {
                }
            }


            Motion();
        }

        private void OnEnable()
        {
            _input.CharacterControls.Enable();
        }

        private void OnDisable()
        {
            _input.CharacterControls.Disable();
        }

        // was macht das???
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Collectable") && pickUpPressed)
            {
                Debug.Log("kurva");

                var script = other.gameObject.GetComponent<ItemPickUp>();
                script.ePressed = true;
            }

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("attack"))
            {
                _plhe.health -= 5;
            }
            if (other.gameObject.CompareTag("storyone"))
            {
                SceneManager.LoadScene(sceneName);
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("BearHit"))//Bear macht mehr schaden
            {
                _plhe.health -= 10;
            }
        }


        private void OnRotationInput(InputAction.CallbackContext context)
        {
            _currentRotationInput = context.ReadValue<Vector2>();
            _currentRotation.x = _currentRotationInput.x;
            _currentRotation.y = _currentRotationInput.y;
            _isRotationPressed = _currentRotationInput.x != 0 || _currentRotationInput.y != 0;
        }

        private void Motion()
        {
            var currentMaxVelocity = _runPressed ? MaximumRunVelocity : MaximumWalkVelocity;
            if (forwardPressed && _velocityZ < currentMaxVelocity) _velocityZ += Time.deltaTime * acceleration;

            if (leftPressed && _velocityX > -currentMaxVelocity) _velocityX -= Time.deltaTime * acceleration;
            if (rightPressed && _velocityX < currentMaxVelocity) _velocityX += Time.deltaTime * acceleration;
            if (_backPressed && _velocityZ > -currentMaxVelocity) _velocityZ -= Time.deltaTime * acceleration;
            if (!forwardPressed && _velocityZ > 0.0f) _velocityZ -= Time.deltaTime * deceleration;
            if (!forwardPressed && _velocityZ > 0.0f) _velocityZ -= Time.deltaTime * deceleration;
            if (!_backPressed && _velocityZ < 0.0f) _velocityZ += Time.deltaTime * deceleration;
            if (!leftPressed && _velocityX < 0.0f) _velocityX += Time.deltaTime * deceleration;
            if (!rightPressed && _velocityX > 0.0f) _velocityX -= Time.deltaTime * deceleration;

            if (!leftPressed && !rightPressed && _velocityX != 0.0f && _velocityX > -0.05f && _velocityX < 0.05f)
                _velocityX = 0.0f;
            if (!forwardPressed && !_backPressed && _velocityZ != 0.0f && _velocityZ > -0.05f && _velocityZ < 0.05f)
                _velocityZ = 0.0f;


            if (forwardPressed && _runPressed && _velocityZ > currentMaxVelocity)
            {
                _velocityZ = currentMaxVelocity;
            }
            else if (forwardPressed && _velocityZ > currentMaxVelocity)
            {
                _velocityZ -= Time.deltaTime * deceleration;
                if (_velocityZ > currentMaxVelocity && _velocityZ < currentMaxVelocity + 0.05f)
                    _velocityZ = currentMaxVelocity;
            }
            else if (forwardPressed && _velocityZ < currentMaxVelocity && _velocityZ > currentMaxVelocity - 0.05f)
            {
                _velocityZ = currentMaxVelocity;
            }


            if (rightPressed && _runPressed && _velocityX > currentMaxVelocity)
            {
                _velocityX = currentMaxVelocity;
            }
            else if (rightPressed && _velocityX > currentMaxVelocity)
            {
                _velocityX -= Time.deltaTime * deceleration;
                if (_velocityX > currentMaxVelocity && _velocityX < currentMaxVelocity + 0.05f)
                    _velocityX = currentMaxVelocity;
            }
            else if (rightPressed && _velocityX < currentMaxVelocity && _velocityX > currentMaxVelocity - 0.05f)
            {
                _velocityX = currentMaxVelocity;
            }


            if (leftPressed && _runPressed && _velocityX < -currentMaxVelocity)
            {
                _velocityX = -currentMaxVelocity;
            }
            else if (leftPressed && _velocityX < -currentMaxVelocity)
            {
                _velocityX += Time.deltaTime * deceleration;
                if (_velocityX < -currentMaxVelocity && _velocityX > -currentMaxVelocity - 0.05f)
                    _velocityX = -currentMaxVelocity;
            }
            else if (leftPressed && _velocityX > -currentMaxVelocity && _velocityX < -currentMaxVelocity + 0.05f)
            {
                _velocityX = -currentMaxVelocity;
            }


            if (_backPressed && _runPressed && _velocityZ < -currentMaxVelocity)
            {
                _velocityZ = -currentMaxVelocity;
            }
            else if (_backPressed && _velocityZ < -currentMaxVelocity)
            {
                _velocityZ += Time.deltaTime * deceleration;
                if (_velocityZ < -currentMaxVelocity && _velocityZ > -currentMaxVelocity - 0.05f)
                    _velocityZ = -currentMaxVelocity;
            }
            else if (_backPressed && _velocityZ > -currentMaxVelocity && _velocityZ < -currentMaxVelocity + 0.05f)
            {
                _velocityZ = -currentMaxVelocity;
            }


            _animator.SetFloat("VelocityZ", _velocityZ);
            _animator.SetFloat("VelocityX", _velocityX);
        }

        private void HandleRotation()
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


            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.transform.eulerAngles.y, ref _turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
        }

        private void HandleMovement()
        {
            var isRunning = _animator.GetBool(_isRunningHash);
            var isWalking = _animator.GetBool(_isWalkingHash);
            var isShooting = _animator.GetBool(Shoot);
            var isBow = _animator.GetBool(ArrowStand);
            var wpressed = Input.GetKey(KeyCode.W);
            var isJumping = _animator.GetBool(Jump);
            var isSneaking = _animator.GetBool(Sneak);

            if (_movementPressed && !isWalking)
            {
                haus = true;
                _animator.SetBool(_isWalkingHash, true);
                _animator.SetBool(Shoot, false);
            }

            if (!_movementPressed && isWalking)
            {
                _animator.SetBool(_isWalkingHash, false);
                _currentrotation = transform.rotation;
                haus = false;
            }

            if (_movementPressed && _runPressed && !isRunning)
            {
                haus = true;
                _animator.SetBool(_isRunningHash, true);
                _animator.SetBool(Shoot, false);
            }

            if ((!_movementPressed || !_runPressed) && isRunning) _animator.SetBool(_isRunningHash, false);
            if (_attackPressed && !isShooting)
            {
                sword.SetActive(true);
                bow.SetActive(false);
                attackdamage1 = true;
                _animator.SetBool(Shoot, true);
            }

            if (!_attackPressed) _animator.SetBool(Shoot, false);
            if (!_attackPressed && !_movementPressed)
            {
                //animator.SetBool("Idle",true);
            }
            else
            {
                _animator.SetBool(Idle, false);
            }

            switch (attackBowPressed)
            {
                case true when !isBow:
                    // AimCam.gameObject.SetActive(true);

                    // ThirdPersonCam.gameObject.SetActive(false);
                    // AimCam.m_XAxis.Value = ThirdPersonCam.m_XAxis.Value;
                    // AimCam.m_YAxis.Value = ThirdPersonCam.m_YAxis.Value;

                    _animator.SetBool(ArrowStand, true);
                    _canRelease = true;
                    _arr = Instantiate(Arrow, refPoint.transform.position, refPoint.transform.rotation);
                    _arr.transform.Rotate(new Vector3(0, 0, 90));
                    _arr.transform.parent = refPoint.transform;
                    _rb = _arr.GetComponent<Rigidbody>();
                    _rb.isKinematic = true;
                    break;
                case false:
                    _animator.SetBool(ArrowStand, false);
                    break;
            }


            if (_attackReleased)
            {
                //ThirdPersonCam.gameObject.SetActive(true);
                //AimCam.gameObject.SetActive(false);
                //  ThirdPersonCam.VerticalAxis.Value = AimCam.m_VerticalAxis.Value;
                //ThirdPersonCam.m_YAxis.Value = AimCam.m_YAxis.Value;
                _rb.isKinematic = false;

                Debug.Log("geht" + _arrowTimer);
                _arr.transform.parent = null;

                _arrow = _arr.GetComponent<Arrow>();

                _arrow.Shoot(_arrowTimer * 40);
                _attackReleased = false;
                _canRelease = false;
                _arrowTimer = 0;
            }


            _animator.SetBool(Jump, jumpPressed);

            //BookAttack
            if (_firstbook && _book.attackFinished && _plhe.mana > price1)
            {
                _book.attackFinished = false;
                _plhe.mana -= price1;
                _book.AttackBook(book1);
            }

            if (_secondbook && _book.attackFinished && _plhe.mana > price2)
            {
                _book.attackFinished = false;
                _plhe.mana -= price2;
                _book.AttackBook(book2);
            }

            if (_thirdbook && _book.attackFinished && _plhe.mana > price3)
            {
                _book.attackFinished = false;
                _plhe.mana -= price3;
                _book.AttackBook(book3);
            }
        }
    }
}