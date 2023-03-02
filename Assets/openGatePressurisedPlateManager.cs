
using UnityEngine;

public class openGatePressurisedPlateManager : MonoBehaviour
{
    [SerializeField] private DoorMovement OpenDoor;
    [SerializeField] private string[] listOfTagsToDetect;
    [SerializeField] private int[] listOfLayersToDetect;
    [SerializeField] private openGatePressurisedPlate[] plates;

    private bool doorOpen = false;


    private void Start()
    {
        foreach (var plate in plates)
        {
            plate.listOfTagsToDetect = listOfTagsToDetect;
            plate.listOfLayersToDetect = listOfLayersToDetect;
        }
    }


    private void Update()
    {
        if (allPlatesIsActivate())
        {
            if (!doorOpen)
            {
                doorOpen = true;
                openDoor();
            }
        }
        else
        {
            if (doorOpen)
            {
                doorOpen = false;
                closedDoor();
            }
        }
    }

    private bool allPlatesIsActivate()
    {
        foreach (var plate in plates)
        {
            if (!plate.playerDetected)
            {
                return false;
            }
        }
        return true;
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
