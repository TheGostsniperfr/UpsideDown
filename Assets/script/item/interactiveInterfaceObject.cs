using Mirror;
using UnityEngine;


public abstract class interactiveInterfaceObject : NetworkBehaviour
{


    //type of the interaction : open, dring, eat, ... 
    public abstract KeyCode getKey(PlayerData playerData);

    //description of the object show on ui 

    public abstract string getDescription();
    public abstract void onAction();
}
