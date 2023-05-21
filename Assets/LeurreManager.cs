using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeurreManager : interactiveInterfaceObject
{
    [SerializeField] CharacterMoveAB CharacterMoveAB;
    [SerializeField] float timeToWait = 5f;

    [SerializeField] float cooldownTime = 3f;
    private float lastTimeUsed = -1f;

    [SerializeField] Transform point1;
    [SerializeField] Transform point2;

    public override string getDescription()
    {
        return "Attracted the drone";
    }

    public override KeyCode getKey(PlayerData playerData)
    {
        return playerData.useKey;
    }

    public override void onAction()
    {
        //check cooldown
        if(lastTimeUsed < Time.timeSinceLevelLoad)
        {
            lastTimeUsed = Time.timeSinceLevelLoad;
            CharacterMoveAB.StartLeurre(point1, point2, timeToWait);
        }
    }

   
}
