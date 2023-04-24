using Mirror;
using UnityEngine;

public class pickUpIObject : NetworkBehaviour
{
    [Header("InteractableInfo")]
    public float sphereCastRadius = 0.5f;
    public int interactableLayerIndex;
    private Vector3 raycastPos;
    public GameObject lookObject;
    private isPickUp physicsObject;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private playerController playerController;

    [Header("Pickup")]
    [SerializeField] private Transform pickupParent;
    public GameObject currentlyPickedUpObject;
    private Rigidbody pickupRB;
    [SerializeField] private Player player;
    [SerializeField] private float maxDistancePickUp = 10f;

    [Header("SFX")]
    [SerializeField] private playerSoundManager soundManager;


    //A simple visualization of the point we're following in the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(pickupParent.position, 0.5f);
    }

    //Interactable Object detections and distance check
    void Update()
    {

        if (isLocalPlayer)
        {
            //Here we check if we're currently looking at an interactable object
            raycastPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.SphereCast(raycastPos, sphereCastRadius, mainCamera.transform.forward, out hit, maxDistancePickUp, 1 << interactableLayerIndex))
            {

                lookObject = hit.collider.transform.root.gameObject;
            }
            else
            {
                lookObject = null;
            }

            if (physicsObject != null && physicsObject.player == this.player)
            {
                if (physicsObject.gravityObject != this.playerController.gravity)
                {
                    physicsObject.synLocalGravity(this.playerController.gravity);
                }
            }

            //get key for action
            PlayerData playerData = player.playerData;

            //if we press the button of choice
            if (Input.GetKeyDown(playerData.lockObjectKey))
            {
                //and we're not holding anything
                if (currentlyPickedUpObject == null)
                {
                    //and we are looking an interactable object
                    if (lookObject != null)
                    {
                        soundManager.isGrab();
                        PickUpObject();

                    }

                }
                //if we press the pickup button and have something, we drop it
                else
                {
                    soundManager.isGrab();
                    BreakConnection();
                }
            }

        }


    }

    //Release the object
    public void BreakConnection()
    {

        physicsObject.BreakConnection();
        currentlyPickedUpObject = null;
        physicsObject.player = null;
    }

    public void PickUpObject()
    {
        physicsObject = lookObject.GetComponentInChildren<isPickUp>();
        physicsObject.setPickUpIObject(this.player);
        currentlyPickedUpObject = lookObject;

    }




}
