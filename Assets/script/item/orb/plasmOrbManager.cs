using Mirror;
using UnityEngine;

public class plasmOrbManager : NetworkBehaviour
{
    [SerializeField] private isPickUp isPickUpComponent;
    public orbSupportManager orbEmitterSupportManager;

    private void OnTriggerEnter(Collider col)
    {

        if (isPickUpComponent.player == null && col.gameObject.tag != "Player" && col.gameObject.layer != 16 && col.gameObject.tag != "volume" && col.gameObject.tag != "orb")
        {
            explosion();
            Debug.Log("EXPLOSION : " + col.gameObject.name);

        }
    }

    [Command(requiresAuthority = false)]
    private void CmdExplosion(GameObject player)
    {
        Player playerComponent = player.GetComponent<Player>();

        playerComponent.RpcTakeDamage((int)playerComponent.currentHealth + 1);
    }

    public void explosion()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            CmdExplosion(player);

        }

        //teleport the orb at the start
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.transform.position = orbEmitterSupportManager.orbSpawnArea.transform.position;
        isPickUpComponent.BreakConnection();
        isPickUpComponent.resetGravity();
    }
}
