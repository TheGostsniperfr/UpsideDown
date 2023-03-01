using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPointPad : MonoBehaviour
{
    public bool playerDetected { get; private set; } = false;


    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerDetected = false;
        }
    }


}
