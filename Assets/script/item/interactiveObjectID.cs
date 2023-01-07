using UnityEngine;

public class interactiveObjectID : MonoBehaviour 
{
    //name of item, not used
    //[SerializeField] private string name = "unknown";

    //description of the object show on ui 
    public string description = "open door";

    //type of the interaction : open, dring, eat, ... 
    public KeyCode getKey(PlayerData playerData)
    {
        return playerData.useKey;
    }
}
