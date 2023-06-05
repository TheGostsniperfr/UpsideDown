
using System.Collections.Generic;
using UnityEngine;

public class FourGeneratorGate : MonoBehaviour
{
    [SerializeField] private List<Active_Generator> active_Generators;
    [SerializeField] private DoorMovement doorMovement;
    [SerializeField] private List<GameObject> tpPoints;

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

                if (questCompleted(networkSync.questLevel))
                {
                    //open the door for the tp portail
                    openDoor();
                }
                else
                {
                    //tp the player into the end room
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                    for (int i = 0; i < 2; i++)
                    {
                        Rigidbody rb = players[i].GetComponent<Rigidbody>();
                        rb.velocity = Vector3.zero;

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
}
