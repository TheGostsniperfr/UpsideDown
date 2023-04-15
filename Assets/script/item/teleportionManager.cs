using Mirror;
using System.Collections;
using UnityEngine;

public class teleportionManager : NetworkBehaviour
{
    [SerializeField] private GameObject[] portails;
    [SerializeField] private string sceneNameToLoad;
    private bool isStartToTP = false;

    //gameLevel = -1 -> don't change the globalGameLevel
    //gameLevel > 1 -> change if superior the global game level
    [SerializeField] private int gameLevel = -1;

    private bool allPortailsIsDetetected()
    {
        foreach (var portail in portails)
        {
            if (!portail.GetComponent<triggerDetectObject>().isDetected) { return false; }
        }
        return true;
    }

    private void Update()
    {
        if (allPortailsIsDetetected() && !isStartToTP)
        {
            isStartToTP = true;

            checkGameLevel();

            Debug.Log("player go to tp in : " + sceneNameToLoad);

            NetworkServer.SendToAll(new loadingScreenMSG());

            StartCoroutine(changeScene());
        }
    }
    private IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1);
        NetworkManager.singleton.ServerChangeScene(sceneNameToLoad);
    }

    private void checkGameLevel()
    {
        if (gameLevel == -1) { return; }

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManager.currentGameLevel < gameLevel)
        {
            gameManager.currentGameLevel = gameLevel;
        }

    }

    public void setStateOfPortails(bool state)
    {
        foreach (var portail in portails)
        {
            portail.SetActive(state);
        }
    }

}
