using UnityEngine;

public class FlammeStreamManager : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;

    private float lastHit = 0;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit player");
        if (other.tag == "Player" && lastHit + cooldown < Time.timeSinceLevelLoad)
        {
            other.GetComponent<Player>().RpcTakeDamage(damage);
            lastHit = Time.timeSinceLevelLoad;

        }
    }



}
