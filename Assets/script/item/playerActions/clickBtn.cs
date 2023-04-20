
using Mirror;
using UnityEngine;

public class clickBtn : interactiveInterfaceObject
{
    [Header("Global settings")]
    [SyncVar] public bool isClicked = false;


    [Header("Time settings")]
    public bool useTime = false;
    public float tickOfTheBtn = 1f;
    public float timeLastClick = -1000f;

    [Header("Sound")]
    [SerializeField] private AudioSource pressBtn;
    private bool check = false;


    public override string getDescription()
    {
        return "Press the button";
    }

    [Command(requiresAuthority = false)]
    public void CmdisClicked(bool state)
    {
        isClicked = state;
    }

    private void Update()
    {
        if (isClicked && !check)
        {
            timeLastClick = Time.timeSinceLevelLoad;
            check = true;
        }

        if (useTime)
        {
            if (check && timeLastClick + tickOfTheBtn < Time.timeSinceLevelLoad)
            {
                check = false;
                CmdisClicked(false);
            }

            if (!isClicked)
            {
                check = false;
            }
        }
    }

    public override void onAction()
    {
        Debug.Log("Btn pressed ! ");

        if (pressBtn != null)
        {
            pressBtn.Play();
        }
        CmdisClicked(true);
    }

    public override KeyCode getKey(PlayerData playerData)
    {
        return playerData.useKey;
    }
}
