using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    [SerializeField] private string noRendering = "noRendering";
    [SerializeField] private string rendering = "player";

    [SerializeField] private GameObject body;
    [SerializeField] private GameObject hair;
    [SerializeField] private GameObject noBody;

    Camera sceneCamera;

    [SerializeField] private Animator anim;

    [SerializeField] private GameObject playerUIPrefab;
    public GameObject playerUIInstance;

    [SerializeField] private playerController playerController;



    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
            //create ui
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.GetComponent<PauseMenu>().setup(gameObject.GetComponent<Player>());

        }
    }




    public override void OnStartClient()
    {
        base.OnStartClient();
        GameManager.RegisterPlayer(GetComponent<NetworkIdentity>().netId.ToString(), GetComponent<Player>());

    }



    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);

        body.layer = LayerMask.NameToLayer(rendering);
        hair.layer = LayerMask.NameToLayer(rendering);
        noBody.layer = LayerMask.NameToLayer(noRendering);
    }

    private void DisableComponents()
    {
        // On va boucler sur les différents composants renseignés et les désactiver si ce joueur n'est pas le notre
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            anim.applyRootMotion = false;
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        Destroy(playerUIInstance);

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);
    }


}