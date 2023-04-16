using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public Transform MainDoor, LeftDoor, RightDoor;
    public bool TriggerOpeningDoor;
    public bool TriggerClosingDoor;
    public float moveSpeed;
    public float sizeOfDoor = 1.2f;
    public float amountOfDoorInFrame = 1.1f;

    private Vector3 MainDoorClose, LeftDoorClose, RightDoorClose;
    private Vector3 MainDoorOpen, LeftDoorOpen, RightDoorOpen;
    public float startTime;
    private float totalDistanceToCover;

    public AudioSource gateSound;


    // Start is called before the first frame update
    void Start()
    {
        MovementOfTheDoor();
        TriggerClosingDoor = false;
        TriggerOpeningDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoors();
        CloseDoors();
    }

    void MovementOfTheDoor()
    {
        MainDoorClose = MainDoor.localPosition;
        LeftDoorClose = LeftDoor.localPosition;
        RightDoorClose = RightDoor.localPosition;

        MainDoorOpen = new Vector3(
            MainDoor.localPosition.x,
            MainDoor.localPosition.y + (sizeOfDoor * amountOfDoorInFrame),
            MainDoor.localPosition.z);

        LeftDoorOpen = new Vector3(
            LeftDoor.localPosition.x,
            LeftDoor.localPosition.y,
            LeftDoor.localPosition.z + (sizeOfDoor * amountOfDoorInFrame));

        RightDoorOpen = new Vector3(
            RightDoor.localPosition.x,
            RightDoor.localPosition.y,
            RightDoor.localPosition.z - (sizeOfDoor * amountOfDoorInFrame));

        totalDistanceToCover = Vector3.Distance(LeftDoorClose, RightDoorOpen);
    }

    void OpenDoors()
    {
        if (TriggerOpeningDoor)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / totalDistanceToCover;
            MainDoor.localPosition = Vector3.LerpUnclamped(MainDoor.localPosition, MainDoorOpen, fractionOfJourney);
            LeftDoor.localPosition = Vector3.LerpUnclamped(LeftDoor.localPosition, LeftDoorOpen, fractionOfJourney);
            RightDoor.localPosition = Vector3.LerpUnclamped(RightDoor.localPosition, RightDoorOpen, fractionOfJourney);

            if (Mathf.Approximately(MainDoor.localPosition.y, MainDoorOpen.y))
            {
                Debug.Log("Doors Opened");
            }

        }
    }

    void CloseDoors()
    {
        if (TriggerClosingDoor)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / totalDistanceToCover;
            MainDoor.localPosition = Vector3.LerpUnclamped(MainDoor.localPosition, MainDoorClose, fractionOfJourney);
            LeftDoor.localPosition = Vector3.LerpUnclamped(LeftDoor.localPosition, LeftDoorClose, fractionOfJourney);
            RightDoor.localPosition = Vector3.LerpUnclamped(RightDoor.localPosition, RightDoorClose, fractionOfJourney);

            if (Mathf.Approximately(MainDoor.localPosition.y, MainDoorClose.y))
            {
                Debug.Log("Doors Closed");
            }

        }
    }
}
