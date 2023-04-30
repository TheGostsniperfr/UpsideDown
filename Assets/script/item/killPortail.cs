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
        }
        else
        {
            Destroy(gameObject);
        }


        Debug.Log(gameObject.name + " has been kill by " + this.name);
    }


}
