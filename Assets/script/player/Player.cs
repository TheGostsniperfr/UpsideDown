using Mirror;
using System.Collections;
using TMPro;
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

    public int defaultGravity = 1;

    [Header("player hp")]

    [SyncVar]
    public float currentHealth;

    [SerializeField] private int playerMaxHealth = 100;


    [Header("Regen")]
    [SerializeField] private bool regenActivate = true;
    [SerializeField] private float cooldownRegen = 1f;
    [SerializeField] private float hpRegenByCycle = 10f;
    [SerializeField] private float CooldownActiveRegenAfterDamage = 3f;
    [SerializeField] public BillBoard billBoard;

    private float timeLastCycle;
    private float timeLastDamage;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerSetup playerSetup;


    [Header("player keyBind")]
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
    [SerializeField] private AudioSource hitSound;

    [Header("username")]
    [SerializeField, SyncVar] private string syncPlayerName;
    [SerializeField] TMP_Text text;



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


            checkGravity();

            if (bloodScreenImg == null && playerSetup.playerUIInstance != null)
            {
                bloodScreenImg = playerSetup.playerUIInstance.gameObject.GetComponent<bloodScreenManager>().image;
            }
        }

        if (isLocalPlayer && syncPlayerName == string.Empty)
        {
            changePlayerName(PlayerPrefs.GetString("PlayerName", "Unknown"));
        }


        if (text.text != syncPlayerName)
        {
            text.text = syncPlayerName;
        }
    }

    [Command(requiresAuthority = false)]
    private void changePlayerName(string newName)
    {
        syncPlayerName = newName;
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
        newHealth(playerMaxHealth);
        currentHealth = playerMaxHealth;

        //check gravity
        if (playerController.gravity != defaultGravity)
        {
            if (defaultGravity == 1)
            {
                playerController.playerGraphics.transform.localRotation = Quaternion.Euler(0, 0, 0);

            }
            else
            {
                playerController.playerGraphics.transform.localRotation = Quaternion.Euler(0, 0, 180);
            }

            playerController.gravitySwited = !playerController.gravitySwited;
            playerController.gravity *= -1;
        }
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
        StartCoroutine(changeHealth(playerMaxHealth));
    }

    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {
        if (isLocalPlayer)
        {
            if (isDead)
            {
                return;
            }
            float bloodScreenCurrentHealth = currentHealth;
            newHealth(bloodScreenCurrentHealth - amount);
            currentHealth = bloodScreenCurrentHealth - amount;


            //reset regen cooldown
            timeLastDamage = Time.time;

            //play sound damage
            hitSound.Play();

            Debug.Log(transform.name + " a maintenant : " + currentHealth + " points de vies.");
            StartCoroutine(changeHealth(bloodScreenCurrentHealth));


            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }


    [Command(requiresAuthority = false)]
    private void newHealth(float hp)
    {
        currentHealth = hp;
    }

    private void Die()
    {
        isDead = true;

        playerController.EnablePlayerInput(false);

        pickUpIObject pickUpIObjectComponent = this.gameObject.GetComponent<pickUpIObject>();
        if (pickUpIObjectComponent.currentlyPickedUpObject != null)
        {
            pickUpIObjectComponent.BreakConnection();
        }

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
                newHealth(playerMaxHealth);
                currentHealth = playerMaxHealth;
            }
            else
            {
                var result = currentHealth + hpRegenByCycle;
                newHealth(result);
                currentHealth = result;
            }

            //pb here
            StartCoroutine(changeHealth(bloodScreenCurrentHealth));

            timeLastCycle = Time.time;
        }
    }

    private IEnumerator changeHealth(float _currentHealth)
    {
        yield return new WaitForSeconds(0.1f);


        float time = 0;


        while (time < 1f)
        {
            float alpha = 1f - (Mathf.Lerp(_currentHealth, currentHealth, time) / playerMaxHealth);

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
        StopCoroutine(changeHealth(0));

    }



    private void checkPlayerActions()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, playerReatch))
        {
            if (keyPositions == null)
            {
                keyPositions = playerSetup.playerUIInstance.gameObject.GetComponent<KeyPositions>();
            }

            if (hit.collider != null)
            {
                Tags tags;
                hit.collider.gameObject.TryGetComponent<Tags>(out tags);

                //check new interactif object ( ex: door, tv, ... )
                if ((hit.collider.gameObject.tag == "interactive" || (tags != null && tags.HasTag("interactive"))) && hit.collider.gameObject != currentHit)
                {
                    if (currentHit != null)
                    {
                        keyPositions.removeKeyUI(currentKey);
                    }

                    currentHit = hit.collider.gameObject;

                    currentInterfaceObject = currentHit.GetComponent<interactiveInterfaceObject>();
                    //var outLine = currentHit.GetComponent<OutlineObject>();

                    //outLine.enabled = true;

                    currentKey = currentInterfaceObject.getKey();

                    keyPositions.ShowKeyUI(currentKey, currentInterfaceObject.getDescription());
                }
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


    public void showDialogue(DialogueObject dialogueObject)
    {
        if (isLocalPlayer)
        {
            dialogueSystem dialogue = playerSetup.playerUIInstance.gameObject.GetComponent<PauseMenu>()._dialogueSystem;

            //show the new dialogue if no dialogue is playing
            if (!dialogue.dialogueIsPlaying)
            {
                dialogue.ShowDialogue(dialogueObject);
            }
        }
    }
}
