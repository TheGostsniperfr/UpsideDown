using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToClose : MonoBehaviour
{
    public DoorMovement OpenDoor;

    private void OnTriggerExit(Collider other)
    {
        OpenDoor.startTime = Time.time;
        OpenDoor.TriggerClosingDoor = true;
        OpenDoor.TriggerOpeningDoor = false;
    }
}
