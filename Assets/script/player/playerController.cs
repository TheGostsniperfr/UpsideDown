
using Mirror;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : NetworkBehaviour
{
    [Header("Player mouvement")]
    //component
    [SerializeField] private Rigidbody rb;

    //isGrounded
    [SerializeField] private float distanceIsGrounded = 1f;

    //rotation
    [SerializeField] private float mouseSensitivityX = 5f;
    [SerializeField] private float mouseSensitivityY = 5f;
    private float rotationX = 0f;

    //Sphere rotation
    [SerializeField] private GameObject gravitySphere;

    //walk / jump
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private float playerCurrentSpeed = 3f;
    [SerializeField] private float playerSprintSpeed = 4.5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool playerInputControlKeyBoardBool = true;
    [SerializeField] private bool playerInputControlMouseBool = true;



    private Vector3 currentInputControl;
    private Vector3 smoothInputVelocity;
    private Vector3 playerInputControl;
    [SerializeField] private float currentSmoothInputSpeed = 0.05f;
    [SerializeField] private float jumpSmoothInputSpeed = 1f;
    [SerializeField] private float smoothInputSpeed = 0.2f;

    [Header("Gravity switch")]
    [SerializeField] public int gravity = 1;
    [SerializeField] private bool gravitySwited;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private GameObject playerGraphics;
    [SerializeField] private bool eneableSwitchGravity = true;


    //gravity player rotation
    [SerializeField] private float gravitRotationSpeed = 1f;

    [Header("camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float camRotMin = -90f;
    [SerializeField] private float camRotMax = 90f;

    [Header("Animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private NetworkAnimator netAnimator;

    [Header("player keyBind")]
    public PlayerData playerData;

    [Header("Player No Gravity Mode")]
    [SerializeField] private bool isNoGravity = false;
    [SerializeField] private CapsuleCollider capsuleCollider;

    [SerializeField] private PhysicMaterial defaultPhysic;
    [SerializeField] private PhysicMaterial bouncePhysic;
    [SerializeField] private float decreaseVelocity = 1.3f;
    private RigidbodyConstraints defaultContrain;
    [SerializeField] private float timeBeforeDisableNoGravity = 3f;
    private float timeWithNoMoveWithNoGravity;
    private bool isNoMovingWithNoGravity = false;

    //raycast of the player 
    RaycastHit hit;


    [Header("Sound")]
    [SerializeField] private playerSoundManager soundManager;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerSpeed = playerCurrentSpeed;
        smoothInputSpeed = currentSmoothInputSpeed;


        defaultContrain = rb.constraints;
    }

    private void Start()
    {
        playerData = JSONSaving.loadSettings();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            movePlayer();
            rotationPlayer();
            rotationCamera();

            gravitySwitcher();

            noGravityMode();

            //check if the player is on a plateform
            playerOnMovingPlatform();

            //Debug.Log("is grounded : " + isGrounded());


            //cheat code for portails
            if (Input.GetKeyDown(KeyCode.J))
            {
                try
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().currentGameLevel = 7;
                }
                catch { }

            }

        }
    }


    private void FixedUpdate()
    {
        //check if the collider is a stair ( layer stair : 15 )
        if (hit.collider != null && hit.collider.gameObject.layer == 15) { return; }


        if (!isNoGravity)
        {
            rb.AddForce(Physics.gravity * rb.mass * gravity);
        }
    }


    public void noGravityMode()
    {
        if ((isGrounded() || isNoGravity) && Input.GetKeyDown(playerData.NoGravityKey))
        {
            //check current mode of the player
            if (isNoGravity)
            {
                setNoGravity(false);

            }
            else
            {
                setNoGravity(true);
            }
        }
    }


    private void setNoGravity(bool state)
    {
        if (state)
        {
            isNoGravity = true;
            rb.constraints = (RigidbodyConstraints)(112 + 4);

            capsuleCollider.material = bouncePhysic;
        }
        else
        {
            isNoGravity = false;
            rb.constraints = defaultContrain;

            capsuleCollider.material = defaultPhysic;
        }
        animator.SetBool("noGravity", isNoGravity);

    }

    public void EnablePlayerInput(bool status)
    {
        if (status)
        {
            //enable player input 
            playerInputControlKeyBoardBool = true;
            playerInputControlMouseBool = true;
            eneableSwitchGravity = true;
        }
        else
        {
            //disable player input
            playerInputControlKeyBoardBool = false;
            playerInputControlMouseBool = false;
            eneableSwitchGravity = false;

            //disable sprint and reset speed sprint
            isSprinting = false;
            playerSpeed = playerCurrentSpeed;
        }
    }
    private IEnumerator smoothRotation()
    {
        var playerOrigineRotation = playerGraphics.transform.localEulerAngles;
        var playerRotation = new Vector3(playerGraphics.transform.localEulerAngles.x + 180, playerGraphics.transform.localEulerAngles.y + 180, playerGraphics.transform.localEulerAngles.z);

        //var graviRotationOrigine = gravitySphere.transform.localEulerAngles;
        //var graviRotation = new Vector3(graviRotationOrigine.x + 180, graviRotationOrigine.y, graviRotationOrigine.z);


        float time = 0f;

        while (time < 1f)
        {
            playerGraphics.transform.localEulerAngles = Vector3.Slerp(playerOrigineRotation, playerRotation, time);
            //gravitySphere.transform.eulerAngles = Vector3.Slerp(graviRotationOrigine, graviRotation, time);

            time += Time.deltaTime * gravitRotationSpeed;
            yield return null;
        }
        StopCoroutine(smoothRotation());

        //fix def the rotation

        if (gravitySwited)
        {
            playerGraphics.transform.localRotation = Quaternion.Euler(0, 0, 180);
            //gravitySphere.transform.rotation = Quaternion.Euler(180, 0, 0);

        }
        else
        {
            playerGraphics.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //gravitySphere.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }





    private bool isGrounded()
    {
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void playerOnMovingPlatform()
    {
        if (isGrounded() && !isNoGravity)
        {
            if (hit.collider != null && hit.collider.gameObject.layer == 13)
            {
                gameObject.transform.SetParent(hit.collider.gameObject.transform, true);
            }
            else
            {
                gameObject.transform.SetParent(null);
            }

        }

    }

    private void movePlayer()
    {
        Physics.Raycast(transform.position, Vector3.down * gravity, out hit, distanceIsGrounded);

        if (isGrounded())
        {
            smoothInputSpeed = currentSmoothInputSpeed;
        }
        else
        {
            smoothInputSpeed = jumpSmoothInputSpeed;
        }


        //sprint system
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!isSprinting)
            {
                isSprinting = true;
                playerSpeed = playerSprintSpeed;
            }
        }
        else
        {
            if (isSprinting)
            {
                playerSpeed = playerCurrentSpeed;
                isSprinting = false;

            }
        }


        if (!animator.GetBool("isRunning") && isSprinting && isGrounded())
        {
            animator.SetBool("isRunning", true);
            soundManager.isRunning(true);
        }
        else if (animator.GetBool("isRunning") && !isSprinting || !isGrounded())
        {
            animator.SetBool("isRunning", false);
            soundManager.isRunning(false);

            if (animator.GetBool("isWalking"))
            {
                soundManager.isWalking(true);
            }
        }

        if (playerInputControlKeyBoardBool)
        {
            playerInputControl = new Vector3(Input.GetAxis("Horizontal") * gravity, 0f, Input.GetAxis("Vertical"));
        }
        else
        {
            playerInputControl = new Vector3(0f, 0f, 0f);
        }



        if (!isNoGravity)
        {
            Vector3 MoveVector = transform.TransformDirection(currentInputControl) * playerSpeed;

            currentInputControl = Vector3.SmoothDamp(currentInputControl, playerInputControl, ref smoothInputVelocity, smoothInputSpeed);


            rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);

            if (MoveVector.magnitude > 0.1)
            {
                if (!animator.GetBool("isWalking") && isGrounded())
                {
                    soundManager.isWalking(true);
                    animator.SetBool("isWalking", true);

                    if (Input.GetKey(KeyCode.Q))
                    {
                        animator.SetBool("walkLeft", true);
                    }
                    else
                    {
                        animator.SetBool("walkLeft", false);

                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        animator.SetBool("walkRight", true);
                    }
                    else
                    {
                        animator.SetBool("walkRight", false);

                    }

                }

            }
            else
            {

                soundManager.isWalking(false);


                animator.SetBool("isWalking", false);
                animator.SetBool("walkRight", false);
                animator.SetBool("walkLeft", false);
            }

            //jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded() && (hit.collider.gameObject.tag != "cube" || hit.collider.gameObject.tag != "Player"))
                {
                    rb.AddForce(Vector3.up * jumpForce * gravity, ForceMode.Impulse);
                    netAnimator.SetTrigger("jump");

                    animator.SetBool("isWalking", false);
                    animator.SetBool("walkRight", false);
                    animator.SetBool("walkLeft", false);

                    soundManager.isRunning(false);
                    soundManager.isWalking(false);
                }
            }

        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

            //decrease the velocity
            rb.velocity /= decreaseVelocity;

            if (rb.velocity.magnitude < 0.1f)
            {

                //disable the no gravity is the player stay motionless after time 
                if (!isNoMovingWithNoGravity)
                {
                    isNoMovingWithNoGravity = true;
                    timeWithNoMoveWithNoGravity = Time.timeSinceLevelLoad;
                }
                else if (timeWithNoMoveWithNoGravity + timeBeforeDisableNoGravity < Time.timeSinceLevelLoad)
                {

                    setNoGravity(false);

                    isNoMovingWithNoGravity = false;
                }

                rb.velocity = Vector3.zero;
            }

            //if the player get velocity after no moving ( ex : collision )
            else if (isNoMovingWithNoGravity)
            {
                isNoMovingWithNoGravity = false;
            }


        }



    }
    private void rotationPlayer()
    {
        if (playerInputControlMouseBool)
        {
            transform.rotation *= Quaternion.Euler(0f, Input.GetAxis("Mouse X") * mouseSensitivityX * gravity, 0f);
        }
    }
    private void rotationCamera()
    {
        if (playerInputControlMouseBool)
        {
            rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivityY;
            rotationX = Mathf.Clamp(rotationX, camRotMin, camRotMax);
            cam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }
    private void gravitySwitcher()
    {
        if (eneableSwitchGravity && Input.GetKeyDown(playerData.switchGravityKey) && isGrounded() && SceneManager.GetActiveScene().name != "Hub")
        {
            soundManager.isSwitchingGragity();
            gravitySwited = !gravitySwited;
            gravity *= -1;
            netAnimator.SetTrigger("flip");
            StartCoroutine(smoothRotation());
        }

    }
}
