using UnityEngine;




[System.Serializable]
public class PlayerData
{
    [Header("KeyBinding for interactive object")]
    public KeyCode useKey = KeyCode.F;
    public KeyCode switchGravityKey = KeyCode.E;
    public KeyCode lockObjectKey = KeyCode.Mouse1;
    public KeyCode NoGravityKey = KeyCode.G;
    public KeyCode nextDialogue = KeyCode.W;
    public KeyCode testKey = KeyCode.O;

    [Header("keyBinding for dialogues")]
    public KeyCode reponse1 = KeyCode.Y;
    public KeyCode reponse2 = KeyCode.N;
    public KeyCode reponse3 = KeyCode.H;



}


