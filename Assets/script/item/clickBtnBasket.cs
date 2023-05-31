using Mirror;

using UnityEngine;

public class clickBtnBasket : interactiveInterfaceObject
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

    [SerializeField] private basketDeplacement basket;


    public override string getDescription()
    {
        return "Press the button";
    }

    [Command(requiresAuthority = false)]
    public void CmdisClicked()
    {
        Debug.Log("cmd click btn");
        basket.reverseNow();
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
        CmdisClicked();
    }

    public override KeyCode getKey()
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(ActionForKeys.useKey));
    }
}
