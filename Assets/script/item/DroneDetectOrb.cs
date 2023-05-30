
using UnityEngine;

public class DroneDetectOrb : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "orb")
        {
            col.gameObject.GetComponent<plasmOrbManager>().explosion();
        }
    }
}
