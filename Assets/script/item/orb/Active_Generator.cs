using Mirror;
using UnityEngine;

public class Active_Generator : NetworkBehaviour
{

    [SerializeField] private GameObject ring1;
    [SerializeField] private GameObject ring2;
    [SerializeField] private GameObject ring3;
    [SerializeField] private GameObject ring4;

    private Vector3 axeY = new Vector3(0, 1, 0);
    private Vector3 axeX = new Vector3(1, 0, 0);
    private Vector3 axeDiag = new Vector3(1, 1, 0);

    private float speed = 50f;

    [SerializeField] private GameObject orbTexture;

    //receiver mode : 
    [SerializeField] private DoorMovement doorMovement;

    [SerializeField] private bool cheatMode = false;
    [SerializeField, SyncVar] private bool isActive = false;

    void Update()
    {
        if (isActive)
        {
            activeGenerator();
            orbTexture.SetActive(true);

            if (!doorMovement.TriggerOpeningDoor)
            {
                openDoor();
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "orb")
        {
            CmdActiveGenerator();
            NetworkManager.Destroy(col.gameObject);
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdActiveGenerator()
    {
        isActive = true;
    }


    private void openDoor()
    {
        if (doorMovement != null)
        {
            doorMovement.startTime = Time.time;
            doorMovement.TriggerOpeningDoor = true;
            doorMovement.TriggerClosingDoor = false;
        }
    }

    private void activeGenerator()
    {
        ring1.transform.Rotate(axeY * speed * Time.deltaTime);
        ring2.transform.Rotate(axeX * speed * Time.deltaTime);
        ring3.transform.Rotate(axeDiag * speed * Time.deltaTime);
        ring4.transform.Rotate(axeDiag * -speed * Time.deltaTime);
    }
}
