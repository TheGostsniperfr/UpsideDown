using Mirror;
using UnityEngine;

public class plasmOrbManager : NetworkBehaviour
{
    [SerializeField] private isPickUp isPickUpComponent;
    public orbSupportManager orbEmitterSupportManager;

    private void OnTriggerEnter(Collider col)
    {
        if (isPickUpComponent.player == null && col.gameObject.tag != "Player" && col.gameObject.layer != 16)
        {
            Debug.Log("EXPLOSION");

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in players)
            {
                Player playerComponent = player.GetComponent<Player>();

                playerComponent.RpcTakeDamage((int)playerComponent.currentHealth + 1);
            }

            orbEmitterSupportManager.enabled = true;
            NetworkServer.Destroy(this.gameObject);
        }
    }
}
