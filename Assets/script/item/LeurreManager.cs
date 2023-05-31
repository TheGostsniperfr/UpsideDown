using Mirror;
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

    public override KeyCode getKey()
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(ActionForKeys.useKey));

    }

    [Command(requiresAuthority = false)]
    public override void onAction()
    {
        //check cooldown
        if (lastTimeUsed + cooldownTime < Time.timeSinceLevelLoad)
        {
            lastTimeUsed = Time.timeSinceLevelLoad;
            CharacterMoveAB.StartLeurre(point1, point2, timeToWait);
        }
    }



}
