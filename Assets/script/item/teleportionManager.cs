using Mirror;
using UnityEngine;

public class teleportionManager : NetworkBehaviour
{
    [SerializeField] private triggerDetectObject[] portails;
    [SerializeField] private string sceneNameToLoad;
    [SerializeField] private NetworkManager networkManager;

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
        if (allPortailsIsDetetected())
        {
            Debug.Log("player go to tp in : " + sceneNameToLoad);
            networkManager.ServerChangeScene(sceneNameToLoad);
        }
    }

}
