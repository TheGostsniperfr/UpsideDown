
using UnityEngine;

public class clickBtnManager : MonoBehaviour
{
    [Header("Global settings")]
    [SerializeField] private DoorMovement OpenDoor;
    [SerializeField] private clickBtn[] listBtn;
    [SerializeField] private bool keepDoorOpen = false;

    [Header("Time settings")]
    [SerializeField] private bool useTime = false;
    [SerializeField] private float timeToWait = 1f;
    private float timeLastClick;
    private bool isWaiting = false;

    [SerializeField] private bool reverseMode = false;
    private bool isOpen = false;

    private void Start()
    {
        if (useTime)
        {
            foreach (var btn in listBtn)
            {
                btn.useTime = useTime;
            }
        }

        if (reverseMode)
        {
            isOpen = false;
        }
    }


    private void Update()
    {
        if (!useTime)
        {
            var tempIsOpen = isOpen;

            if (allIsActivate())
            {
                isOpen = true;
            }
            else if (!keepDoorOpen)
            {
                isOpen = false;
            }

            if (tempIsOpen != isOpen)
            {
                if (isOpen)
                {
                    openDoor();
                }
                else
                {
                    closedDoor();
                }
            }
        }
        else
        {
            if (!isWaiting)
            {
                if (allIsActivate())
                {
                    openDoor();
                    timeLastClick = Time.timeSinceLevelLoad;
                    isWaiting = true;
                }
            }
            else
            {
                if (!keepDoorOpen && timeLastClick + timeToWait < Time.timeSinceLevelLoad)
                {
                    isWaiting = false;
                    closedDoor();
                }
            }
        }

    }

    private bool allIsActivate()
    {
        for (int i = 0; i < listBtn.Length; i++)
        {
            if (listBtn[i].isClicked == false)
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
