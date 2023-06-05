
using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class FourGeneratorGate : NetworkBehaviour
{
    [SerializeField] private List<Active_Generator> active_Generators;
    [SerializeField] private DoorMovement doorMovement;
    [SerializeField] private List<GameObject> tpPoints;
    public DialogueObject dialogueToShow1;
    public DialogueObject dialogueToShow2;



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
        if(!openSomethings && allIsActive()) 
        {
            openSomethings = true;

            //check if the player finish the quest
            try
            {
                NetworkSync networkSync = GameObject.Find("networkSyncObj").GetComponent<NetworkSync>();

                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                if (questCompleted(networkSync.questLevel))
                {
                    //open the door for the tp portail
                    openDoor();
                    foreach (var player in players)
                    {
                        showDialogueToAll(player, dialogueToShow1);

                    }
                }
                else
                {
                    //tp the player into the end room

                    for (int i = 0; i < 2; i++)
                    {
                        Rigidbody rb = players[i].GetComponent<Rigidbody>();
                        rb.velocity = Vector3.zero;
                        showDialogueToAll(players[i], dialogueToShow2);

                        players[i].transform.position = tpPoints[i].transform.position; 
                    }
                }
            }
            catch { Debug.LogError("no networkSyncObj found"); }
        }
    }


    private bool questCompleted(List<bool> a)
    {
        foreach(bool b in a)
        {
            if(!b)
            {
                return false;   
            }
        }

        return true;
    }

    private bool allIsActive()
    {
        foreach (var generator in active_Generators)
        {
            if(!generator.isActive) { return false; }
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
