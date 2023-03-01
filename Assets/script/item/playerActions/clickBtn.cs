
using UnityEngine;

public class clickBtn : interactiveInterfaceObject
{
    [Header("Global settings")]
    public bool isClicked = false;
    [SerializeField] private bool reverseMode = false;


    [Header("Time settings")]
    public bool useTime = false;
    public float tickOfTheBtn = 1f;
    private float timeLastClick;
    private bool isWaiting = false;


    public override string getDescription()
    {
        return "Press the button";
    }


    private void Update()
    {
        if (timeLastClick + tickOfTheBtn < Time.timeSinceLevelLoad)
        {
            isWaiting = false;
            isClicked = false;
        }
    }



    public override void onAction()
    {
        Debug.Log("Btn pressed ! ");

        if (!useTime)
        {
            isClicked = !isClicked;
        }
        else
        {
            isClicked = true;
            isWaiting = true;
            timeLastClick = Time.timeSinceLevelLoad;
        }




    }

    public override KeyCode getKey(PlayerData playerData)
    {
        return playerData.useKey;
    }






}
