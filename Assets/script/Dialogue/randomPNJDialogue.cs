
using System.Collections.Generic;
using UnityEngine;

public class randomPNJDialogue : MonoBehaviour
{
    [SerializeField] private List<DialogueObject> dialogueObjects;
    private int lastIndex = -1;

    private void OnTriggerEnter(Collider col)
    {
        int newIndex;
        do 
        { 
            newIndex = Random.Range(0, dialogueObjects.Count); 
        }
        while (newIndex == lastIndex);

        lastIndex = newIndex;

        //show dialogue
        col.gameObject.GetComponent<Player>().showDialogue(dialogueObjects[newIndex]);
    }
}
