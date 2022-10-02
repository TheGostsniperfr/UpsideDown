
using System.Collections;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Player mouvement")]
    //component
    [SerializeField] private CharacterController characterController;

    //isGrounded
    [SerializeField] private float distanceIsGrounded = 1f;


    //rotation
    [SerializeField] private float mouseSensitivityX = 5f;
    [SerializeField] private float mouseSensitivityY = 5f;
    private float rotationX = 0f;

    //walk / jump
    [SerializeField] private Vector3 playerVelocity;
    private Vector3 playerInput;
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private float playerSprintSpeedMultiplicator = 1.5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isSprinting;

    [Header("Gravity switch")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private bool gravitySwited;

    //gravity player rotation
    [SerializeField] private float gravitRotationSpeed = 1f;




    [Header("camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float camRotMin = -90f;
    [SerializeField] private float camRotMax = 90f;


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

    private IEnumerator smoothRotation()
    {
        Quaternion playerRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        if (gravitySwited)
        {
            playerRotation = Quaternion.Euler(new Vector3(0, 0, 180));
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
        if(Physics.Raycast(transform.position, -transform.up, distanceIsGrounded) || characterController.isGrounded)
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
            }
        }
        else
        { 
            if (isSprinting)
            {
                playerSpeed /= playerSprintSpeedMultiplicator;
                isSprinting = false;
            }
        }



        playerInput = new Vector2(playerSpeed * Input.GetAxis("Vertical"), playerSpeed * Input.GetAxis("Horizontal"));

        float playerDirectionY = playerVelocity.y;
        playerVelocity = (transform.TransformDirection(Vector3.forward) * playerInput.x) + (transform.TransformDirection(Vector3.right) * playerInput.y);
        playerVelocity.y = playerDirectionY;



        //jump system
        if (isGrounded())
        {
            playerVelocity.y = -1f;
            if (gravitySwited)
            {
                playerVelocity.y = 1f;
            }


                if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("jump");
                if (!gravitySwited) 
                {
                    playerVelocity.y = jumpForce;
                }
                else
                {
                    playerVelocity.y = -jumpForce;
                }
            }
        }
        else
        {
            playerVelocity.y -= gravity * -2f * Time.deltaTime;

        }

        characterController.Move(playerVelocity * Time.deltaTime);
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
            StartCoroutine(smoothRotation());
         

        }
        
    }
}
