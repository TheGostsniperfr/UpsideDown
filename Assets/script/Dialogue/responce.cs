
using UnityEngine;

[System.Serializable]
public class Responce
{
    [SerializeField] private string reponseText;
    [SerializeField] private DialogueObject dialogueObject;

    public string ResponceText => reponseText;
    public DialogueObject DialogueObject => dialogueObject;
    
}
