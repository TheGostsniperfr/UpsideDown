
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
    public bool gravitySwited;
    [SerializeField] private CapsuleCollider playerCollider;
    public GameObject playerGraphics;
    [SerializeField] private bool eneableSwitchGravity = true;

    [Header("camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float camRotMin = -90f;
    [SerializeField] private float camRotMax = 90f;

    [Header("Animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private NetworkAnimator netAnimator;

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
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            movePlayer();
            rotationPlayer();
            rotationCamera();

            gravitySwitcher();


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



        rb.AddForce(Physics.gravity * rb.mass * gravity);

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

        float time = 0f;

        while (time < 1f)
        {
            playerGraphics.transform.localEulerAngles = Vector3.Slerp(playerOrigineRotation, playerRotation, time);

            time += Time.deltaTime * PlayerPrefs.GetFloat("rorationSpeed");
            yield return null;
        }
        StopCoroutine(smoothRotation());

        //fix def the rotation

        if (gravitySwited)
        {
            playerGraphics.transform.localRotation = Quaternion.Euler(0, 0, 180);

        }
        else
        {
            playerGraphics.transform.localRotation = Quaternion.Euler(0, 0, 0);
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
        if (isGrounded())
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
        if (Input.GetKeyDown(KeyCode.Space) && playerInputControlKeyBoardBool)
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
    private void rotationPlayer()
    {
        if (playerInputControlMouseBool)
        {
            transform.rotation *= Quaternion.Euler(0f, Input.GetAxis("Mouse X") * PlayerPrefs.GetFloat("mouseSensiX") * gravity, 0f);
        }
    }
    private void rotationCamera()
    {
        if (playerInputControlMouseBool)
        {
            rotationX -= Input.GetAxis("Mouse Y") * PlayerPrefs.GetFloat("mouseSensiY");
            rotationX = Mathf.Clamp(rotationX, camRotMin, camRotMax);
            cam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }
    private void gravitySwitcher()
    {
        if (eneableSwitchGravity && Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(ActionForKeys.switchGravity))) && isGrounded() && SceneManager.GetActiveScene().name != "Hub")
        {



            soundManager.isSwitchingGragity();
            gravitySwited = !gravitySwited;
            gravity *= -1;
            netAnimator.SetTrigger("flip");
            StartCoroutine(smoothRotation());
        }

    }
}
