using Mirror;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerLobby : NetworkManager
{
    [Scene][SerializeField] private string menuScene = string.Empty;

    [Header("Game")]
    [SerializeField] private GameObject playerSpawnSystem = null;


    public static event Action OnClientConected;
    public static event Action OnClientDisconnected;
    public static event Action<NetworkConnectionToClient> OnServerReadied;
    public static event Action OnNewPlayerConnected;



    public override void OnStartClient()
    {
        base.OnStartClient();


    }


    public override void OnClientConnect()
    {
        base.OnClientConnect();
        OnClientConected?.Invoke();
        OnNewPlayerConnected?.Invoke();

    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();

        OnClientDisconnected?.Invoke();
    }


    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        if (SceneManager.GetActiveScene().path != menuScene)
        {
            conn.Disconnect();
            return;
        }

    }


    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            NetworkServer.AddPlayerForConnection(conn, Instantiate(this.playerPrefab));
        }


    }


    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName.StartsWith(""))
        {

            GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
            NetworkServer.Spawn(playerSpawnSystemInstance);
        }
    }



    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        base.OnServerReady(conn);

        OnServerReadied?.Invoke(conn);
    }



}
