
using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class FourGeneratorGate : NetworkBehaviour
{
    [SerializeField] private List<Active_Generator> active_Generators;
    [SerializeField] private DoorMovement doorMovement;
    [SerializeField] private List<GameObject> tpPoints;
    public DialogueObject dialogueToShowDie;
    public DialogueObject dialogueToShowRoom7;



    private bool openSomethings = false;



    private void Start()
    {
        if (tpPoints.Count >= 2)
        {
            Debug.LogError("no enough tp points");
        }
    }

    private void Update()
    {
        if (!openSomethings && allIsActive())
        {
            openSomethings = true;

            //check if the player finish the quest
            try
            {
                NetworkSync networkSync = GameObject.Find("networkSyncObj").GetComponent<NetworkSync>();

                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                if (networkSync.itemQuest0 == 1 && networkSync.itemQuest1 == 1 && networkSync.itemQuest2 == 1 && networkSync.itemQuest3 == 1)
                {
                    //open the door for the tp portail
                    openDoor();
                    foreach (var player in players)
                    {
                        showDialogueToAll(player, dialogueToShowRoom7);

                    }
                }
                else
                {
                    //tp the player into the end room

                    for (int i = 0; i < 2; i++)
                    {
                        Rigidbody rb = players[i].GetComponent<Rigidbody>();
                        rb.velocity = Vector3.zero;
                        showDialogueToAll(players[i], dialogueToShowDie);

                        players[i].transform.position = tpPoints[i].transform.position;
                    }
                }
            }
            catch { Debug.LogError("no networkSyncObj found"); }
        }
    }


    private bool allIsActive()
    {
        foreach (var generator in active_Generators)
        {
            if (!generator.isActive) { return false; }
        }
        return true;
    }

    private void openDoor()
    {
        if (doorMovement != null)
        {
            doorMovement.startTime = Time.time;
            doorMovement.TriggerOpeningDoor = true;
            doorMovement.TriggerClosingDoor = false;
        }
    }

    [Command(requiresAuthority = false)]
    private void showDialogueToAll(GameObject player, DialogueObject dialogueObject)
    {
        player.GetComponent<Player>().showDialogue(dialogueObject);
    }
}
