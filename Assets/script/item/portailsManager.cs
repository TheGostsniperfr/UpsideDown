using UnityEngine;

public class portailsManager : MonoBehaviour
{
    [SerializeField] private teleportionManager[] teleportionManagers;

    public int levelDeblocked = 1;

    [SerializeField] private GameManager gameManager;

    private void Update()
    {

        if (gameManager == null)
        {
            try
            {
                gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                levelDeblocked = gameManager.currentGameLevel;
                refreshPortails();
            }
            catch { }
        }

        if (gameManager != null && levelDeblocked < gameManager.currentGameLevel)
        {
            levelDeblocked = gameManager.currentGameLevel;
            refreshPortails();

            Debug.Log("level deblocked : " + levelDeblocked);
        }

    }



    public void refreshPortails()
    {
        for (int i = levelDeblocked - 1; i < teleportionManagers.Length; i++)
        {
            teleportionManagers[i].setStateOfPortails(false);
        }

        for (int j = 0; j < levelDeblocked; j++)
        {
            teleportionManagers[j].setStateOfPortails(true);
        }
    }
}
