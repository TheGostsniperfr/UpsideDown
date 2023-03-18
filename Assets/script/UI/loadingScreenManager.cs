using Mirror;
using UnityEngine;

public class loadingScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreenUI;


    private void Start()
    {
        NetworkClient.RegisterHandler<loadingScreenMSG>(showLoadingScreen);
    }

    public void showLoadingScreen(loadingScreenMSG msg)
    {
        Debug.Log("show loading screen");
        loadingScreenUI.SetActive(true);
    }
}
