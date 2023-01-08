using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




[System.Serializable]
public class PlayerData
{
    [Header("KeyBinding for interactive object")]
    public KeyCode useKey = KeyCode.F;
    public KeyCode switchGravityKey = KeyCode.E;
    public KeyCode nextDialogue = KeyCode.W;
    public KeyCode testKey = KeyCode.O;

    [Header("keyBinding for dialogues")]
    public KeyCode reponse1 = KeyCode.Y;
    public KeyCode reponse2 = KeyCode.N;
    public KeyCode reponse3 = KeyCode.H;
}


