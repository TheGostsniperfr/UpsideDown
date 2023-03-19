using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPanePanel = null;

    public void HostLobby()
    {
        networkManager.StartHost();
        landingPanePanel.SetActive(false);
    }


    public void play()
    {
        //load first scene
        SceneManager.LoadScene("SampleScene");
    }

    public void settings()
    {
        //load settings
        Debug.Log("settings btn pressed");
    }

    public void quit()
    {
        //quit the game
        Application.Quit();
    }
}
