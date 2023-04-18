using Mirror;
using UnityEngine;

public class orbSupportManager : NetworkBehaviour
{
    [SerializeField] private bool isEmitter = false;
    [SerializeField] private GameObject orbObj;
    [SerializeField] private GameObject orbSpawnArea;
    [SerializeField] private orbSupportManager orbSupportEmitter;

    //emitter mode : 
    private GameObject orbInstance;

    //receiver mode : 
    [SerializeField] private DoorMovement doorMovement;
    [SerializeField] private bool isOpening = true;
    [SerializeField] private GameObject orbEmitInstance;

    void Update()
    {
        if (isEmitter && orbInstance == null)
        {
            orbInstance = Instantiate(orbObj);
            orbInstance.transform.position = orbSpawnArea.transform.position;
            orbInstance.gameObject.GetComponent<plasmOrbManager>().orbEmitterSupportManager = this;
            NetworkServer.Spawn(orbInstance);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!isEmitter && col.gameObject.tag == "orb" && (orbEmitInstance == null || col.gameObject != orbEmitInstance))
        {

            if (isOpening)
            {
                openDoor();
            }

            if (orbSupportEmitter != null)
            {
                orbSupportEmitter.enabled = false;
            }

            NetworkServer.Destroy(col.gameObject);

            orbEmitInstance = Instantiate(orbObj);
            orbEmitInstance.transform.position = orbSpawnArea.transform.position;
            orbEmitInstance.gameObject.GetComponent<plasmOrbManager>().orbEmitterSupportManager = orbSupportEmitter;

            NetworkServer.Spawn(orbEmitInstance);
        }
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
}
