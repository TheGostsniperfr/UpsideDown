using Mirror;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AStarAgent))]
public class CharacterMoveAB : NetworkBehaviour
{
    AStarAgent _Agent;

    [SerializeField] List<Transform> rdPoints = new List<Transform>();
    [SerializeField] private Transform currentPoint;
    private int index = 0;

    [SerializeField] private isPickUp isPickUp;


    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    [SerializeField] Vector3 LeurrePosition;
    [SerializeField] float LeurreTime = -1f;
    private bool GoToLeurre = false;


    [SerializeField] private float cooldownToUpdatePath = 3f;
    private float lastUpdatePath = 0f;



    private void Start()
    {
        _Agent = GetComponent<AStarAgent>();

        chooseNewRandomPoint();
    }




    private void Update()
    {
        //check leurre
        if (Time.timeSinceLevelLoad < LeurreTime)
        {
            if (!GoToLeurre)
            {
                nextLeurrePoint();
                _Agent.Pathfinding(LeurrePosition);
                GoToLeurre = true;
            }

            if (_Agent.Status != AStarAgentStatus.Finished)
            {
                return;
            }

            if (_Agent.Status == AStarAgentStatus.Invalid || _Agent.Status == AStarAgentStatus.Finished)
            {
                nextLeurrePoint();
                _Agent.Pathfinding(LeurrePosition);
            }
        }
        else
        {
            GoToLeurre = false;

            //check if the player grab the orb
            if (isPickUp != null && isPickUp.pickedUp)
            {
                //go to player

                if (lastUpdatePath + cooldownToUpdatePath < Time.timeSinceLevelLoad)
                {
                    lastUpdatePath = Time.timeSinceLevelLoad;

                    _Agent.Pathfinding(isPickUp.gameObject.transform.position);

                    if(_Agent.Status == AStarAgentStatus.Invalid || _Agent.Status == AStarAgentStatus.Finished)
                    {
                        chooseNewRandomPoint();
                        _Agent.Pathfinding(currentPoint.position);
                    }
                }

                return;
            }
            else
            {
                //normal mode
                if (_Agent.Status != AStarAgentStatus.Finished)
                {
                    return;
                }


                if (_Agent.Status == AStarAgentStatus.Invalid || _Agent.Status == AStarAgentStatus.Finished)
                {
                    chooseNewRandomPoint();
                    _Agent.Pathfinding(currentPoint.position);
                }
                
            }
        }
    }


    private void chooseNewRandomPoint()
    {
        int tempIndex = index;
        while (tempIndex == index)
        {
            index = Random.Range(0, rdPoints.Count);
        }

        currentPoint = rdPoints[tempIndex];
    }

    public void StartLeurre(Transform _point1, Transform _point2, float timeToWait)
    {
        this.point1 = _point1;
        this.point2 = _point2;
        LeurreTime = Time.timeSinceLevelLoad + timeToWait;
    }



    private void nextLeurrePoint()
    {

        if (LeurrePosition == point1.position)
        {
            LeurrePosition = point2.position;
        }
        else
        {
            LeurrePosition = point1.position;
        }

    }
}
