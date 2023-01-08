using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




[System.Serializable]
public class PlayerData
{
    [Header("KeyBinding settings")]
    public KeyCode useKey = KeyCode.F;
    public KeyCode switchGravityKey = KeyCode.E;
    public KeyCode nextDialogue = KeyCode.W;
    public KeyCode testKey = KeyCode.O;
}


