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

            if (networkSync != null && networkSync.questLevel[indexOfItem])
            {

                meshRenderer.enabled = true;
            }
        }
    }
}
