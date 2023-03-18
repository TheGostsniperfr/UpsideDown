using UnityEngine;

public class DiscordController : MonoBehaviour
{
    public long applicationID;
    [Space]
    public string details = "Room : ";
    public string state = "";
    [Space]
    public string largeImage = "Icon";
    public string largeText = "Upside Down";

    private long time;

    private static bool instanceExists;
    public Discord.Discord discord;

    private void Awake()
    {
        if (!instanceExists)
        {
            instanceExists = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

        UpdateStatus();



    }

    void Update()
    {
        try
        {
            discord.RunCallbacks();
        }
        catch
        {

            Destroy(this.gameObject);
        }
    }


    private void LateUpdate()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = details,
                State = state,
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText,
                },
                Timestamps =
                {
                    Start = time
                }
            };

            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord");
            });
        }
        catch
        {

            Destroy(this.gameObject);
        }
    }
}
