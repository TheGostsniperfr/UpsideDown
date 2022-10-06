
using System.Collections;
using UnityEngine;

public class playerController : MonoBehaviour
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
    private Vector2 playerMouseInput;

    //walk / jump
    [SerializeField] private Vector3 playerMouvement;
    private Vector3 playerInput;
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private float playerSprintSpeedMultiplicator = 1.5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isSprinting;


    [SerializeField] private LayerMask floorMask;
    [SerializeField] private Transform feetTransform;
    




    [Header("Gravity switch")]
    [SerializeField] private float gravity = 1f;
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
    }

    void Update()
    {
        movePlayer();
        rotationPlayer();
        rotationCamera();

        gravitySwitcher();

    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * rb.mass * gravity);
    }

    private IEnumerator smoothRotation()
    {
        Quaternion playerRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, 0));

        if (gravitySwited)
        {
            playerRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, 180));
        }
  

        float time = -0.2f;

        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, time);

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
        //sprint system
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!isSprinting)
            {
                isSprinting = true;
                playerSpeed *= playerSprintSpeedMultiplicator;
                animator.SetBool("isRunning", true);
            }
        }
        else
        { 
            if (isSprinting)
            {
                playerSpeed /= playerSprintSpeedMultiplicator;
                isSprinting = false;
                animator.SetBool("isRunning", false);

            }
        }

        playerMouvement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Vector3 MoveVector = transform.TransformDirection(playerMouvement) * playerSpeed;
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
        transform.rotation *= Quaternion.Euler(0f, Input.GetAxis("Mouse X") * mouseSensitivityX, 0f);
    }


    private void rotationCamera()
    {
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivityY;
        rotationX = Mathf.Clamp(rotationX, camRotMin, camRotMax);
        cam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
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
