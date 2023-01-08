
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private playerController playerController;
    [SerializeField] private bool menuActivate = false;
    [SerializeField] private GameObject reticule;
    [SerializeField] private GameObject keyPositions;

    private void Awake()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuActivate)
            {
                //Exit menu
                removeMenu();
            }
            else
            {
                //Go to menu
                showMenu();
            }
        }
    }

    private void showMenu()
    {
        //remove reticule and keyPosition
        reticule.SetActive(false);
        keyPositions.SetActive(false);

        //show cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);

        //stop player mouvement
        playerController.EnablePlayerInput(false);


        menuActivate = true;
    }

    private void removeMenu()
    {
        //show reticule and keyPositions
        reticule.SetActive(true);
        keyPositions.SetActive(true);

        //remove cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);

        //enable player mouvement
        playerController.EnablePlayerInput(true);

        menuActivate = false;
    }


    public void resume()
    {
        removeMenu();
    }

    public void setting()
    {
        Debug.Log("settings btn clicked");
    }

    public void menu()
    {
        Debug.Log("menu btn clicked");
    }
}
