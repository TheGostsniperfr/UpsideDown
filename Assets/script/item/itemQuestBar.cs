using UnityEngine;

public class itemQuestBar : MonoBehaviour
{

    private NetworkSync networkSync;
    [SerializeField] private int indexOfItem = 0;
    [SerializeField] private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer.enabled = false;
    }

    private void Update()
    {
        if (!meshRenderer.enabled)
        {
            if (networkSync == null)
            {
                try
                {
                    networkSync = GameObject.Find("networkSyncObj").GetComponent<NetworkSync>();
                }
                catch { }
            }

            if (networkSync != null)
            {

                switch (indexOfItem)
                {
                    case 0:
                        if (networkSync.itemQuest0 == 1) { meshRenderer.enabled = true; }
                        break;
                    case 1:
                        if (networkSync.itemQuest1 == 1) { meshRenderer.enabled = true; }
                        break;
                    case 2:
                        if (networkSync.itemQuest2 == 1) { meshRenderer.enabled = true; }
                        break;
                    case 3:
                        if (networkSync.itemQuest3 == 1) { meshRenderer.enabled = true; }
                        break;

                    default:
                        break;
                }


            }
        }
    }
}
