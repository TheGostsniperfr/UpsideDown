using Mirror;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public BillBoard billBoard;

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
    public Transform pickupParent;
    [SerializeField] private Transform pickupParent1;
    [SerializeField] private Transform pickupParent2;

    public playerController playerController;

    [SerializeField] private Vector3 spawnPointPosition;
    [SerializeField] private Quaternion spawnPointQuaternion;

    [Header("blood screen")]
    [SerializeField] private float speedChangeOpacity = 3f;
    private Image bloodScreenImg;

    private void Awake()
    {
        timeLastCycle = Time.time;
        timeLastDamage = Time.time;
    }

    public void Start()
    {
        SetDefaults();
        checkGravity();

        setSpawnPoint(this.gameObject.transform.position, this.gameObject.transform.rotation);

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
            checkGravity();

            if (bloodScreenImg == null && playerSetup.playerUIInstance != null)
            {
                bloodScreenImg = playerSetup.playerUIInstance.gameObject.GetComponent<bloodScreenManager>().image;
            }
        }
    }

    private void checkGravity()
    {
        if (playerController.gravity == 1)
        {
            pickupParent = pickupParent1;
        }
        else
        {
            pickupParent = pickupParent2;
        }
    }

    public void setSpawnPoint(Vector3 position, Quaternion rotation)
    {
        spawnPointPosition = position;
        spawnPointQuaternion = rotation;
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


        //set new position
        rb.transform.position = spawnPointPosition;
        rb.transform.rotation = spawnPointQuaternion;

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
        float bloodScreenCurrentHealth = currentHealth;
        currentHealth -= amount;

        //reset regen cooldown
        timeLastDamage = Time.time;

        Debug.Log(transform.name + " a maintenant : " + currentHealth + " points de vies.");
        StartCoroutine(changeHealth(bloodScreenCurrentHealth, currentHealth));


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
        if (currentHealth != playerMaxHealth && regenActivate && !isDead && (timeLastCycle + cooldownRegen < Time.time) && (timeLastDamage + CooldownActiveRegenAfterDamage < Time.time))
        {
            float bloodScreenCurrentHealth = currentHealth;

            if (currentHealth + hpRegenByCycle >= playerMaxHealth)
            {
                currentHealth = playerMaxHealth;
            }
            else
            {
                currentHealth += hpRegenByCycle;
            }

            StartCoroutine(changeHealth(bloodScreenCurrentHealth, currentHealth));

            timeLastCycle = Time.time;
        }
    }

    private IEnumerator changeHealth(float currentHealth, float newCurrentHealth)
    {
        float time = 0;


        while (time < 1f)
        {
            float alpha = 1f - (Mathf.Lerp(currentHealth, newCurrentHealth, time) / playerMaxHealth);

            if (alpha > 1)
            {
                alpha = 1;
            }

            if (alpha < 0.01f)
            {
                alpha = 0;
            }

            bloodScreenImg.color = new Color(bloodScreenImg.color.r, bloodScreenImg.color.g, bloodScreenImg.color.b, alpha);


            time += Time.deltaTime * speedChangeOpacity;
            yield return null;
        }
        StopCoroutine(changeHealth(0, 0));

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
                //var outLine = currentHit.GetComponent<OutlineObject>();

                //outLine.enabled = true;

                currentKey = currentInterfaceObject.getKey(playerData);

                keyPositions.ShowKeyUI(currentKey, currentInterfaceObject.getDescription());
            }
        }
        else
        {
            if (currentHit != null)
            {
                //var outLine = currentHit.GetComponent<OutlineObject>();
                //outLine.enabled = false;

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
