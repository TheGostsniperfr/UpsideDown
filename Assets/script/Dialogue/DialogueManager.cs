using Mirror;
using UnityEngine;

public class DialogueManager : NetworkBehaviour
{
    [Header("Dialogue")]
    public DialogueObject dialogueToShow;


    [Header("Launch mode")]

    [SerializeField] bool showToAllPlayer = false;
    [SerializeField] private bool oneActivation = false;
    [SyncVar] private bool wasActivated = false;


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if ((oneActivation && !wasActivated) || !oneActivation)
            {
                if (showToAllPlayer)
                {
                    setWasActivated(true);

                    //show dialogue
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                    foreach (GameObject player in players)
                    {
                        showDialogueToAll(player);
                    }
                }
                else
                {
                    wasActivated = true;

                    //show dialogue
                    col.gameObject.GetComponent<Player>().showDialogue(dialogueToShow);
                }
            }

        }
    }

    [Command(requiresAuthority = false)]
    private void setWasActivated(bool state)
    {
        wasActivated = state;
    }

    [Command(requiresAuthority = false)]
    private void showDialogueToAll(GameObject player)
    {
        player.GetComponent<Player>().showDialogue(dialogueToShow);
    }

}
