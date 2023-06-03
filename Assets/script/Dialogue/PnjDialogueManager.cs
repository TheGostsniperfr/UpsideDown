using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class PnjDialogueManager : NetworkBehaviour
{
    [SerializeField] private List<DialogueObject> dialogueObjects;
    [SerializeField] private NetworkSync networkSync;

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

                    if (dialogueObjects[networkSync.gameLevel - 1] != null)
                    {

                        //show dialogue
                        col.gameObject.GetComponent<Player>().showDialogue(dialogueObjects[networkSync.gameLevel - 1]);
                    }
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
        if (dialogueObjects[networkSync.gameLevel] != null)
        {
            player.GetComponent<Player>().showDialogue(dialogueObjects[networkSync.gameLevel - 1]);
        }
    }
}
