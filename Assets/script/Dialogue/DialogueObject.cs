using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/DialogueObject")]

public class DialogueObject : ScriptableObject
{

    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private Responce[] responces;
           
    public string[] Dialogue => dialogue;

    public bool hasResponses => responces != null && responces.Length > 0;

    public Responce[] Responces => responces;
}
