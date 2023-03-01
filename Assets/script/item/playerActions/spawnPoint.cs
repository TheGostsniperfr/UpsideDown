
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPointPads;

    //mirror spawning game object
    [SerializeField] private GameObject respawnPoint;

    private void Update()
    {
        if (allSpawnPointPadsIsGood())
        {
            respawnPoint.gameObject.transform.position = gameObject.transform.position + new Vector3(0,2,0);
        }


    }

    private bool allSpawnPointPadsIsGood()
    {
        for (int i = 0; i < spawnPointPads.Length; i++)
        {
            if (!spawnPointPads[i].GetComponent<spawnPointPad>().playerDetected)
            {
                return false;
            }
        }
        return true;
    }

}
