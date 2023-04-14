using Mirror;
using System.Collections;
using UnityEngine;

public class isPickUp : NetworkBehaviour
{

    public float waitOnPickup = 0.2f;
    public float breakForce = 5f;
    private int gravityObject = 1;
    [SerializeField] public bool pickedUp = false;
    public pickUpIObject playerInteractions;
    [SerializeField] private Rigidbody rb;

    [SyncVar] public Player player;

    [Header("ObjectFollow")]
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float maxSpeed = 300f;
    [SerializeField] private float maxDistance = 10f;

    private float currentSpeed = 0f;
    private float currentDist = 0f;

    [Header("Rotation")]
    public float rotationSpeed = 100f;
    Quaternion lookRot;


    [Command(requiresAuthority = false)]
    public void setPickUpIObject(Player _player)
    {
        this.player = _player;

        playerInteractions = _player.GetComponent<pickUpIObject>();

        PickUpObject();
    }

    [Command(requiresAuthority = false)]
    public void resetPickUpObject()
    {
        this.player = null;
    }

    //Velocity movement toward pickup parent and rotation
    private void FixedUpdate()
    {
        if (pickedUp)
        {
            currentDist = Vector3.Distance(player.pickupParent.position, rb.position);
            currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentDist / maxDistance);
            currentSpeed *= Time.fixedDeltaTime;
            Vector3 direction = player.pickupParent.position - rb.position;
            rb.velocity = direction.normalized * currentSpeed;

            //Rotation     
            lookRot = Quaternion.LookRotation(player.transform.position - rb.position);
            lookRot = Quaternion.Slerp(player.cam.transform.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);

            rb.MoveRotation(lookRot);
        }

        rb.AddForce(new Vector3(0, -10 * gravityObject, 0), ForceMode.Acceleration);
    }

    //Release the objec
    [Command(requiresAuthority = false)]
    public void BreakConnection()
    {
        rb.constraints = RigidbodyConstraints.None;
        this.pickedUp = false;
        currentDist = 0;
    }
    [Command(requiresAuthority = false)]
    public void PickUpObject()
    {
        this.rb.constraints = RigidbodyConstraints.FreezeRotation;
        StartCoroutine(this.PickUp());
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (pickedUp)
        {
            if (collision.relativeVelocity.magnitude > breakForce)
            {
                playerInteractions.BreakConnection();
            }

        }
    }

    //this is used to prevent the connection from breaking when you just picked up the object as it sometimes fires a collision with the ground or whatever it is touching
    public IEnumerator PickUp()
    {
        yield return new WaitForSecondsRealtime(waitOnPickup);
        pickedUp = true;

    }

    public void synLocalGravity()
    {
        gravityObject = player.playerController.gravity;
    }


    private void Update()
    {
        if (player != null)
        {
            synLocalGravity();
        }


    }
}

