using Mirror;
using System.Collections;
using UnityEngine;

public class teleportionManager : NetworkBehaviour
{
    [SerializeField] private triggerDetectObject[] portails;
    [SerializeField] private string sceneNameToLoad;
    private bool isStartToTP = false;

    private bool allPortailsIsDetetected()
    {
        foreach (var portail in portails)
        {
            if (!portail.isDetected) { return false; }
        }
        return true;
    }

    private void Update()
    {
        if (allPortailsIsDetetected() && !isStartToTP)
        {
            isStartToTP = true;
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





}
