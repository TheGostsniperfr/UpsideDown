
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private playerController playerController;

    [SerializeField] private bool menuActivate = false;

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
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                pauseMenuUI.SetActive(false);

                //enable player mouvement
                playerController.EnablePlayerInput(true);

                menuActivate = false;
            }
            else
            {
                //Go to menu
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pauseMenuUI.SetActive(true);

                //stop player mouvement
                playerController.EnablePlayerInput(false);


                menuActivate = true;
            }
        }
    }


}
