using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AStarAgent))]
public class CharacterMoveAB : MonoBehaviour
{
    AStarAgent _Agent;

    [SerializeField] List<Transform> rdPoints = new List<Transform>();
    [SerializeField] private Transform currentPoint;
    private int index = 0;


    private void Start()
    {
        _Agent = GetComponent<AStarAgent>();       

        chooseNewRandomPoint();
    }

    

    private void Update()
    {
        if(_Agent.Status != AStarAgentStatus.Finished)
        {
            return;
        }

        while (_Agent.Status == AStarAgentStatus.Invalid)
        {            
            chooseNewRandomPoint();    
            _Agent.Pathfinding(currentPoint.position);        
        }

        if(_Agent.Status == AStarAgentStatus.Finished)
        {
            chooseNewRandomPoint();
            _Agent.Pathfinding(currentPoint.position);
        }
    }


    private void chooseNewRandomPoint()
    {
        int tempIndex = index;
        while(tempIndex == index)
        {
            index = Random.Range(0, rdPoints.Count);
        }

        currentPoint = rdPoints[tempIndex]; 
    }
}
