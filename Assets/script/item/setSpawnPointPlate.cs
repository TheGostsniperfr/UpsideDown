using UnityEngine;

public class setSpawnPointPlate : MonoBehaviour
{
    [SerializeField] private GameObject areaSpawnPoint;


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player player = col.gameObject.GetComponent<Player>();
            player.setSpawnPoint(areaSpawnPoint.transform.position, areaSpawnPoint.transform.rotation);
        }
    }

    //Debug view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(areaSpawnPoint.transform.position, 1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(areaSpawnPoint.transform.position, areaSpawnPoint.transform.position + areaSpawnPoint.transform.forward * 3);
    }
}