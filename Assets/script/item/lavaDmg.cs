using UnityEngine;

public class lavaDmg : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;

    private float lastHit = 0;

    private void OnTriggerStay(Collider col)
    {
        Debug.Log("Hit player");
        if (col.tag == "Player" && lastHit + cooldown < Time.timeSinceLevelLoad)
        {
            col.GetComponent<Player>().RpcTakeDamage(damage);
            lastHit = Time.timeSinceLevelLoad;

        }
    }

}
