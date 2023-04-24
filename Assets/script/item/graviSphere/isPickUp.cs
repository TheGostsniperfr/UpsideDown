using Mirror;
using UnityEngine;

public class isPickUp : NetworkBehaviour
{

    public float waitOnPickup = 0.2f;
    public float breakForce = 5f;

    [SerializeField] private Rigidbody rb;

    [SyncVar] public int gravityObject = 1;
    [SyncVar] public Player player;
    [SyncVar] public bool pickedUp = false;
    [SyncVar] public pickUpIObject playerInteractions;

    [Header("ObjectFollow")]
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float maxSpeed = 300f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private int defaultGravity = 1;

    private float currentSpeed = 0f;
    private float currentDist = 0f;


    [Header("Rotation")]
    public float rotationSpeed = 100f;
    Quaternion lookRot;



    [Command(requiresAuthority = false)]
    public void setPickUpIObject(Player _player)
    {
        this.player = _player;
        playerInteractions = player.GetComponent<pickUpIObject>();

        pickedUp = true;
    }

    private void FixedUpdate()
    {
        if (pickedUp)
        {
            this.rb.constraints = RigidbodyConstraints.FreezeRotation;

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

    //Release the object
    [Command(requiresAuthority = false)]
    public void BreakConnection()
    {
        rb.constraints = RigidbodyConstraints.None;
        this.pickedUp = false;
        playerInteractions = null;
        this.player = null;
        currentDist = 0;
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

    [Command(requiresAuthority = false)]
    public void synLocalGravity(int newGravity)
    {
        gravityObject = newGravity;
    }


    public void resetGravity()
    {
        synLocalGravity(defaultGravity);
    }
}

