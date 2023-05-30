using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform mainCameraTransform;
    [SerializeField] private Camera cam;


    private void Start()
    {

    }

    private void Update()
    {
        if (mainCameraTransform != null)
        {
            transform.LookAt(mainCameraTransform);
            transform.Rotate(Vector3.up * 180);
        }
        else
        {
            GameObject[] listPlayers = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject playerGameObject in listPlayers)
            {
                if (playerGameObject.layer == 10)
                {
                    playerGameObject.gameObject.GetComponent<Player>().billBoard.mainCameraTransform = cam.transform;
                }
            }

        }

    }



}
