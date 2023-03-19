using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;
    [Header("UI")]
    [SerializeField] private GameObject landingPanePanel = null;
    [SerializeField] private TMP_InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;

    private void OnEnable()
    {
        NetworkManagerLobby.OnClientConected += NetworkClient.OnConnectedEvent;
        NetworkManagerLobby.OnClientDisconnected += NetworkClient.OnDisconnectedEvent;
    }

    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConected -= NetworkClient.OnConnectedEvent;
        NetworkManagerLobby.OnClientDisconnected -= NetworkClient.OnDisconnectedEvent;
    }

    public void JoinLobby()
    {
        string ipAdress = ipAddressInputField.text;
        networkManager.networkAddress = ipAdress;
        networkManager.StartClient();
        joinButton.interactable = false;
    }

    private void OnConnectedToServer()
    {
        joinButton.interactable = true;
        gameObject.SetActive(false);
        landingPanePanel.SetActive(false);
    }

    private void OnDisconnectedFromServer()
    {
        joinButton.interactable = true;
    }

}
