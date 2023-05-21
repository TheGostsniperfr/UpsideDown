using UnityEngine;

public class killPortail : MonoBehaviour
{
    [SerializeField] private string[] tagsToDestroy;
    [SerializeField] private int[] layersToDestroy;

    private void OnTriggerEnter(Collider col)
    {
        foreach (var tag in tagsToDestroy)
        {
            if (col.gameObject.tag == tag)
            {
                killObject(col.gameObject);
            }
        }

        foreach (var layer in layersToDestroy)
        {
            if (col.gameObject.layer == layer)
            {
                killObject(col.gameObject);
            }
        }
    }

    private void killObject(GameObject gameObject)
    {
        if (gameObject.tag == "Player")
        {
            Player player = gameObject.GetComponent<Player>();
            player.RpcTakeDamage((int)player.currentHealth + 1);

            //check if the player grab the orb
            pickUpIObject pickUp = player.gameObject.GetComponent<pickUpIObject>();

            if (pickUp.gameObject != null &&  pickUp.lookObject.gameObject.tag == "orb")
            {
                pickUp.gameObject.GetComponent<plasmOrbManager>().explosion();
            }
        }
        else
        {
            Destroy(gameObject);
        }


        Debug.Log(gameObject.name + " has been kill by " + this.name);
    }
}
