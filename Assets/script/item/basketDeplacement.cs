
using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class basketDeplacement : NetworkBehaviour
{
    [SerializeField] private List<Vector3> positions;
    public bool isEneable = true;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float waitWhenArrive = 2f;


    private int indexCurrentDestination = 1;
    private float currentWaitTime = 0f;
    private bool isWaiting = false;

    //pathDirection : 1 or -1
    [SyncVar] private int pathDirection = 1;

    [Header("btn function")]
    [SerializeField] private bool useBtn = false;
    [SerializeField] private clickBtnBasket btn;




    private void Start()
    {
        if (positions.Count <= 1)
        {
            Debug.LogError("list of position <= 1 !");
        }
        else
        {
            //set default position
            transform.position = positions[0];
        }

        if (useBtn)
        {
            pathDirection = -1;
        }
    }


    private void Update()
    {
        if (isEneable)
        {
            if (!useBtn)
            {

                if (isWaiting)
                {

                    if (currentWaitTime + waitWhenArrive <= Time.timeSinceLevelLoad)
                    {
                        isWaiting = false;
                    }

                }
                else
                {
                    //check if the basket is arrived at the currentDestination
                    if (transform.position == positions[indexCurrentDestination])
                    {
                        //check if the index is arrived at the limit of the list
                        if (indexCurrentDestination + pathDirection >= positions.Count || indexCurrentDestination + pathDirection < 0)
                        {

                            pathDirection *= -1;

                        }
                        //change the current Destination to the next destination
                        indexCurrentDestination += pathDirection;


                        isWaiting = true;
                        currentWaitTime = Time.timeSinceLevelLoad;



                    }

                    //move basket
                    transform.position = Vector3.MoveTowards(transform.position, positions[indexCurrentDestination], speed * Time.deltaTime);
                }
            }
            else
            {
                //check if the basket is arrived at the end of the path
                if ((pathDirection < 0 && transform.position == positions[0]) || (pathDirection > 0 && transform.position == positions[positions.Count - 1]))
                {
                    return;
                }

                //check if the basket is arrived at the currentDestination
                if (transform.position == positions[indexCurrentDestination])
                {
                    //change the current Destination to the next destination
                    indexCurrentDestination += pathDirection;
                }

                //move basket
                transform.position = Vector3.MoveTowards(transform.position, positions[indexCurrentDestination], speed * Time.deltaTime);
            }
        }
    }

    [Command(requiresAuthority = false)]

    public void reverseNow()
    {
        Debug.Log("reverse now");
        pathDirection *= -1;
    }

}
