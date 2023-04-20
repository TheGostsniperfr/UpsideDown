using Mirror;
using UnityEngine;


public abstract class interactiveInterfaceObject : NetworkBehaviour
{
    //name of item, not used
    //[SerializeField] private string name = "unknown";

    //description of the object show on ui 

    //type of the interaction : open, dring, eat, ... 
    public abstract KeyCode getKey(PlayerData playerData);

    public abstract string getDescription();
    public abstract void onAction();
}
