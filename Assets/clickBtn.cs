
using UnityEngine;

public class clickBtn : interactiveInterfaceObject
{
    [Header("Global settings")]
    [SerializeField] private DoorMovement OpenDoor;
    [SerializeField] private bool isClicked = false;
    [SerializeField] private bool reverseMode = false;
    private float timeLastClick;


    [Header("Time settings")]
    [SerializeField] private bool useTime = false;
    [SerializeField] private float timeToWait = 1f;

    public override string getDescription()
    {
        return "Press the button";
    }

    public override void onAction()
    {
        Debug.Log("Btn pressed ! ");

        if (useTime) 
        {
            if(isClicked)
            {
                if(timeLastClick + timeToWait < Time.timeSinceLevelLoad)
                {
                    isClicked = false;
                    closedDoor();
                }
            }
            else
            {
                isClicked = true;
                timeLastClick = Time.timeSinceLevelLoad;
                openDoor();
            }
        }
        else
        {

            if (isClicked)
            {
                closedDoor();
            }
            else
            {
                openDoor();
            }
            isClicked = !isClicked;
        }
    }

    public override KeyCode getKey(PlayerData playerData)
    {
        return playerData.useKey;
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
