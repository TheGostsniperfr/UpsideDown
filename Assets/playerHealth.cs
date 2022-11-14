
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    [Header("Info player")]
    public float playerCurrentHealth = 100f;
    public bool playerIsAlive = true;
    [SerializeField] private float playerMaxHealth = 100f;


    [Header("Regen")]
    [SerializeField] private bool regenActivate = true;
    [SerializeField] private float cooldownRegen = 1f;
    [SerializeField] private float hpRegenByCycle = 10f;
    [SerializeField] private float CooldownActiveRegenAfterDamage = 3f;

    private float timeLastCycle;
    private float timeLastDamage;

    private void Awake()
    {
        timeLastCycle = Time.time;
        timeLastDamage = Time.time;
    }

    private void Update()
    {
        playerRegen();

        if (Input.GetKeyDown(KeyCode.K))
        {
            playerTakeDamage(30f);
        }
    }

    public void playerTakeDamage(float dmg)
    {
        Debug.Log(gameObject.name + " take " + dmg + " damage");
        if (playerIsAlive)
        {
            if (playerCurrentHealth - dmg <= 0)
            {
                //player died
                playerCurrentHealth = 0;
                playerIsAlive = false;

            }
            else
            {
                //player take dmg
                playerCurrentHealth -= dmg;

            }

            timeLastDamage = Time.time;
        }

    }


    private void playerRegen()
    {
        if (regenActivate && playerIsAlive && (timeLastCycle + cooldownRegen < Time.time) && (timeLastDamage + CooldownActiveRegenAfterDamage < Time.time))
        {
            if(playerCurrentHealth + hpRegenByCycle >= playerMaxHealth)
            {
                playerCurrentHealth = playerMaxHealth;
            }
            else
            {
                playerCurrentHealth += hpRegenByCycle;            
            }

            timeLastCycle = Time.time;
        }
    }
    
}
