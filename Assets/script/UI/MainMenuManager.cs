using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

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
