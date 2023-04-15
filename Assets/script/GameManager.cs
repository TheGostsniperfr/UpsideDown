using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public GameSettings gameSettings;

    public static GameManager instance;

    //game level : progression of the player 
    [SyncVar] public int currentGameLevel = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Debug.LogError("Plus d'une instance de gameManager dans la scène");
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
