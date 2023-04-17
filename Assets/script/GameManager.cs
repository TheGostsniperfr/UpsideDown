using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public GameSettings gameSettings;

    public static GameManager instance;

    //game level : progression of the player 
    public int currentGameLevel = 1;

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
            Debug.Log("get scene name " + SceneManager.GetActiveScene());

            try
            {
                networkSync = GameObject.Find("networkSyncObj").GetComponent<NetworkSync>();
            }
            catch { }
        }

        if (networkSync != null)
        {
            if (networkSync.gameLevel < currentGameLevel)
            {
                networkSync.CmdUpdateScore(currentGameLevel);
            }
            else if (networkSync.gameLevel > currentGameLevel)
            {
                currentGameLevel = networkSync.gameLevel;
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
