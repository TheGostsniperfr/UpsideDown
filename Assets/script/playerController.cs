
using System.Collections;
using UnityEngine;
using Mirror;

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
    [SerializeField] private Vector3 playerMouvement;
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private float playerCurrentSpeed = 3f;
    [SerializeField] private float playerSprintSpeed = 4.5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool playerInputControlBool = true;
    private Vector3 currentInputControl;
    private Vector3 smoothInputVelocity;
    private Vector3 playerInputControl;
    [SerializeField] private float currentSmoothInputSpeed = 0.05f;
    [SerializeField] private float jumpSmoothInputSpeed = 1f;


    [SerializeField] float smoothInputSpeed = 0.2f;
        



    [SerializeField] private LayerMask floorMask;
    [SerializeField] private Transform feetTransform;
    




    [Header("Gravity switch")]
    [SerializeField] public int gravity = 1;
    [SerializeField] private bool gravitySwited;

    //gravity player rotation
    [SerializeField] private float gravitRotationSpeed = 1f;




    [Header("camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float camRotMin = -90f;
    [SerializeField] private float camRotMax = 90f;


    [Header("Animator")]
    [SerializeField] private Animator animator;

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
        }

    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * rb.mass * gravity);
    }

    public void EnablePlayerInput(bool status)
    {
        if (status)
        {
            //enable player input 
            playerInputControlBool = true;
        }
        else
        {
            //disable player input
            playerInputControlBool = false;

            //disable sprint and reset speed sprint
            isSprinting = false;
            playerSpeed = playerCurrentSpeed;

            //reset player inertia ( smooth )
            //playerMouvement = new Vector3(0, 0, 0);
        }
    }


    private IEnumerator smoothRotation()
    {
        Quaternion playerRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0));

        if (gravitySwited)
        {
            playerRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180));
        }
  

        float time = -0.2f;

        while(time < 0.3f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, time);
            gravitySphere.transform.localRotation = transform.rotation;

            time += Time.deltaTime * gravitRotationSpeed;
            yield return null;
        }
        StopCoroutine(smoothRotation());
    }

    private bool isGrounded()
    {
        if(Physics.Raycast(transform.position, Vector3.down * gravity, distanceIsGrounded))
        {
            Debug.Log("IsGrounded");
            return true;
        }
        else
        {
            Debug.Log("Is not Grounded");
            return false;
        }
         
    }


    private void movePlayer()
    {
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
                animator.SetBool("isRunning", true);
            }
        }
        else
        { 
            if (isSprinting)
            {
                playerSpeed = playerCurrentSpeed;
                isSprinting = false;
                animator.SetBool("isRunning", false);

            }
        }

        

        if (playerInputControlBool)
        {
            playerInputControl = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        }
        else
        {
            playerInputControl = new Vector3(0f, 0f, 0f);    
        }

        currentInputControl = Vector3.SmoothDamp(currentInputControl, playerInputControl, ref smoothInputVelocity, smoothInputSpeed);



        Vector3 MoveVector = transform.TransformDirection(currentInputControl) * playerSpeed;
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);

        if(MoveVector.magnitude > 0.1)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);

        }




        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce * gravity, ForceMode.Impulse);
                animator.SetTrigger("jump");
            }
        }

    }





    private void rotationPlayer()
    {
        if (playerInputControlBool)
        {
            transform.rotation *= Quaternion.Euler(0f, Input.GetAxis("Mouse X") * mouseSensitivityX, 0f);
        }
    }


    private void rotationCamera()
    {
        if (playerInputControlBool)
        {
            rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivityY;
            rotationX = Mathf.Clamp(rotationX, camRotMin, camRotMax);
            cam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }


    private void gravitySwitcher()
    {
        if (Input.GetKeyDown(KeyCode.E) && isGrounded())
        {
            gravitySwited = !gravitySwited;
            gravity *= -1;
            animator.SetTrigger("flip");
            StartCoroutine(smoothRotation());
         

        }
        
    }
}
