
using UnityEngine;

public class clickBtn : interactiveInterfaceObject
{
    [Header("Global settings")]
    public bool isClicked = false;


    [Header("Time settings")]
    public bool useTime = false;
    public float tickOfTheBtn = 1f;
    private float timeLastClick;

    [Header("Sound")]
    [SerializeField] private AudioSource pressBtn;


    public override string getDescription()
    {
        return "Press the button";
    }


    private void Update()
    {
        if (useTime && timeLastClick + tickOfTheBtn < Time.timeSinceLevelLoad)
        {
            isClicked = false;
        }
    }



    public override void onAction()
    {
        Debug.Log("Btn pressed ! ");

        if (pressBtn != null)
        {
            pressBtn.Play();
        }

        if (!useTime)
        {
            isClicked = !isClicked;
        }
        else
        {
            isClicked = true;
            timeLastClick = Time.timeSinceLevelLoad;
        }
    }

    public override KeyCode getKey(PlayerData playerData)
    {
        return playerData.useKey;
    }
}
