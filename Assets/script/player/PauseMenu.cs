
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] public playerController playerController;
    [SerializeField] private bool menuActivate = false;
    [SerializeField] private GameObject reticule;
    [SerializeField] private GameObject keyPositions;
    [SerializeField] private GameObject keyBinding;
    [SerializeField] private KeyBindingManager keyBindingsManager;
    [SerializeField] private GameObject settingsMenu;
    public dialogueSystem _dialogueSystem;
    public Player player;




    public void setup(Player _player)
    {
        player = _player;

        playerController = player.gameObject.GetComponent<playerController>();

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

    public void showKeyBindMenu()
    {
        keyBinding.SetActive(true);
        keyBindingsManager.showKey();
        settingsMenu.SetActive(false);
    }

    public void removeKeyBindMeny()
    {
        keyBinding.SetActive(false);
        settingsMenu.SetActive(true);
    }



    public void removeSetting()
    {
        settingsMenu.SetActive(false);
    }


    public void resume()
    {
        removeMenu();
    }

    public void setting()
    {
        //show setting
        settingsMenu.SetActive(true);
    }

    public void quit()
    {
        Debug.Log("quit btn clicked");
        Application.Quit();
    }
}
