using Mirror;
using UnityEngine;

public class orbSupportManager : NetworkBehaviour
{
    public GameObject orbSpawnArea;


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "orb")
        {
            col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            col.gameObject.transform.position = orbSpawnArea.transform.position;
            col.gameObject.GetComponent<isPickUp>().BreakConnection();

        }
    }
}
