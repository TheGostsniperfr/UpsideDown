using UnityEngine;

public class switchOnLight : interactiveInterfaceObject
{
    public override string getDescription()
    {
        return "switch on the light";
    }

    public override void onAction()
    {
        Debug.Log("on action : switch on the light ! ");
    }

    public override KeyCode getKey(PlayerData playerData)
    {
        return playerData.testKey;
    }
}