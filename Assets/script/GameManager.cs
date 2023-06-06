using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public GameSettings gameSettings;

    public static GameManager instance;

    //game level : progression of the player 
    public int currentGameLevel = 1;

    //quest level
    public int currentItemQuest0 = 0;
    public int currentItemQuest1 = 0;
    public int currentItemQuest2 = 0;
    public int currentItemQuest3 = 0;

    private NetworkSync networkSync;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Debug.LogError("Plus d'une instance de gameManager dans la scène");
    }


    void Update()
    {

        if (networkSync == null)
        {
            try
            {
                networkSync = GameObject.Find("networkSyncObj").GetComponent<NetworkSync>();
            }
            catch { }
        }

        if (networkSync != null)
        {

            //check game level
            if (networkSync.gameLevel < currentGameLevel)
            {
                networkSync.CmdUpdateScore(currentGameLevel);
            }
            else if (networkSync.gameLevel > currentGameLevel)
            {
                currentGameLevel = networkSync.gameLevel;
            }



            if (networkSync.itemQuest0 < currentItemQuest0)
            {
                networkSync.CmdUpdateQuest0(currentItemQuest0);
            }
            else if (networkSync.itemQuest0 > currentItemQuest0)
            {
                currentItemQuest0 = networkSync.itemQuest0;
            }

            if (networkSync.itemQuest1 < currentItemQuest1)
            {
                networkSync.CmdUpdateQuest1(currentItemQuest1);
            }
            else if (networkSync.itemQuest1 > currentItemQuest1)
            {
                currentItemQuest1 = networkSync.itemQuest1;
            }

            if (networkSync.itemQuest2 < currentItemQuest2)
            {
                networkSync.CmdUpdateQuest2(currentItemQuest2);
            }
            else if (networkSync.itemQuest2 > currentItemQuest2)
            {
                currentItemQuest2 = networkSync.itemQuest2;
            }

            if (networkSync.itemQuest3 < currentItemQuest3)
            {
                networkSync.CmdUpdateQuest3(currentItemQuest3);
            }
            else if (networkSync.itemQuest3 > currentItemQuest3)
            {
                currentItemQuest3 = networkSync.itemQuest3;
            }
        }
    }


    public void updateCurrentGameLevel(int gameLevel)
    {
        currentGameLevel = gameLevel;
    }

    public static void RegisterPlayer(string netID, Player player)
    {
        string playerId = playerIdPrefix + netID;
        players.Add(playerId, player);
        player.transform.name = playerId;
    }

    public static void UnRegisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    public static Player GetPlayer(string playerID)
    {
        return players[playerID];
    }
}
