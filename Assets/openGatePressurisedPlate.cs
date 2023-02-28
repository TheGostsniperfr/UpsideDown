using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openGatePressurisedPlate : MonoBehaviour
{
    public bool playerDetected { get; private set; } = false;
    [SerializeField] private DoorMovement OpenDoor;
    [SerializeField] private string[] listOfTagsToDetect;
    [SerializeField] private int[] listOfLayersToDetect;



    private void OnTriggerEnter(Collider col)
    {
        for (int i = 0; i < listOfTagsToDetect.Length; i++)
        {
            if (col.gameObject.tag == listOfTagsToDetect[i])
            {
                playerDetected = true;
                openDoor();
            }
        }

        for (int j = 0; j < listOfLayersToDetect.Length; j++)
        {
            if (col.gameObject.layer == listOfLayersToDetect[j])
            {
                playerDetected = true;
                openDoor();
            }
        }        
    }

    private void OnTriggerExit(Collider col)
    {
        for (int i = 0; i < listOfTagsToDetect.Length; i++)
        {
            if (col.gameObject.tag == listOfTagsToDetect[i])
            {
                playerDetected = false;
                closedDoor();
            }
        }

        for (int j = 0; j < listOfLayersToDetect.Length; j++)
        {
            if (col.gameObject.layer == listOfLayersToDetect[j])
            {
                playerDetected = false;
                closedDoor();
            }
        }        
    }


    private void openDoor()
    {
        OpenDoor.startTime = Time.time;
        OpenDoor.TriggerOpeningDoor = true;
        OpenDoor.TriggerClosingDoor = false;
    }

    private void closedDoor()
    {
        OpenDoor.startTime = Time.time;
        OpenDoor.TriggerOpeningDoor = false;
        OpenDoor.TriggerClosingDoor = true;
    }
}
