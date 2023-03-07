using Knife.HDRPOutline.Core;
using Mirror;
using System.Collections;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [Header("player hp")]

    [SyncVar]
    public float currentHealth;

    [SerializeField] private int playerMaxHealth = 100;


    [Header("Regen")]
    [SerializeField] private bool regenActivate = true;
    [SerializeField] private float cooldownRegen = 1f;
    [SerializeField] private float hpRegenByCycle = 10f;
    [SerializeField] private float CooldownActiveRegenAfterDamage = 3f;

    private float timeLastCycle;
    private float timeLastDamage;




    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerSetup playerSetup;


    [Header("player keyBind")]
    public PlayerData playerData;
    [SerializeField] private KeyPositions keyPositions;
    private RaycastHit hit;
    private GameObject currentHit = null;
    private KeyCode currentKey;
    private interactiveInterfaceObject currentInterfaceObject;

    [SerializeField] private float playerReatch = 3f;
    [SerializeField] public Camera cam;

    [Header("pickUp")]
    [SerializeField] public Transform pickupParent;


    [SerializeField] public playerController playerController;




    private void Awake()
    {
        timeLastCycle = Time.time;
        timeLastDamage = Time.time;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            playerRegen();
            checkPlayerActions();

            if (Input.GetKeyDown(KeyCode.K))
            {
                RpcTakeDamage(30);
            }
        }
    }

    public void Start()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        isDead = false;
        currentHealth = playerMaxHealth;
    }

    public ref PlayerData getPlayerData()
    {
        return ref playerData;
    }

    public void reLoadPlayerSettings()
    {
        playerData = JSONSaving.loadData();
    }


    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.gameSettings.respawnTimer);
        SetDefaults();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();

        //disable mouvement ? :

        //set new position
        rb.transform.position = spawnPoint.position;
        rb.transform.rotation = spawnPoint.rotation;

        //enable mouvement ? :
        playerController.EnablePlayerInput(true);

    }



    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= amount;

        //reset regen cooldown
        timeLastDamage = Time.time;

        Debug.Log(transform.name + " a maintenant : " + currentHealth + " points de vies.");


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        playerController.EnablePlayerInput(false);



        Debug.Log(transform.name + " a été éléminé.");
        StartCoroutine(Respawn());
    }


    private void playerRegen()
    {
        if (regenActivate && !isDead && (timeLastCycle + cooldownRegen < Time.time) && (timeLastDamage + CooldownActiveRegenAfterDamage < Time.time))
        {
            if (currentHealth + hpRegenByCycle >= playerMaxHealth)
            {
                currentHealth = playerMaxHealth;
            }
            else
            {
                currentHealth += hpRegenByCycle;
            }

            timeLastCycle = Time.time;
        }
    }


    private void checkPlayerActions()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, playerReatch))
        {
            if (keyPositions == null)
            {
                keyPositions = playerSetup.playerUIInstance.gameObject.GetComponent<KeyPositions>();
            }

            //check new interactif object ( ex: door, tv, ... )
            if (hit.collider != null && hit.collider.gameObject.tag == "interactive" && hit.collider.gameObject != currentHit)
            {
                if (currentHit != null)
                {
                    keyPositions.removeKeyUI(currentKey);
                }

                currentHit = hit.collider.gameObject;

                currentInterfaceObject = currentHit.GetComponent<interactiveInterfaceObject>();
                var outLine = currentHit.GetComponent<OutlineObject>();

                outLine.enabled = true;

                currentKey = currentInterfaceObject.getKey(playerData);

                keyPositions.ShowKeyUI(currentKey, currentInterfaceObject.getDescription());
            }
        }
        else
        {
            if (currentHit != null)
            {
                var outLine = currentHit.GetComponent<OutlineObject>();
                outLine.enabled = false;

                keyPositions.removeKeyUI(currentKey);
                currentHit = null;
            }
        }

        //check if the key bind is pressed
        if (currentHit != null && Input.GetKeyDown(currentKey))
        {
            currentInterfaceObject.onAction();
        }


    }
}
