using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToOpen : MonoBehaviour
{
    public DoorMovement OpenDoor;

    private void OnTriggerEnter(Collider other)
    {
        OpenDoor.startTime = Time.time;
        OpenDoor.TriggerOpeningDoor = true;
        OpenDoor.TriggerClosingDoor = false;
    }
        

}
