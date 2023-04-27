using UnityEngine;

public class HelpUseKey : interactiveInterfaceObject
{
    public string description = "to use this object";

    public override string getDescription()
    {
        return description;
    }

    public override KeyCode getKey(PlayerData playerData)
    {
        return playerData.useKey;
    }

    public override void onAction()
    {

    }
}
