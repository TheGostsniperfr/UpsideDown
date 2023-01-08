using UnityEngine;

public class openDoor : interactiveInterfaceObject
{
    public override string getDescription()
    {
        return "open door";
    }

    public override void onAction()
    {
        Debug.Log("on action : open the door ! ");
    }

    public override KeyCode getKey(PlayerData playerData)
    {
        return playerData.useKey;
    }
}
