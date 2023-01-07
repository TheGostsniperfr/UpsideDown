using UnityEngine;
using System.Collections;
using TMPro;

public class dialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] typeWriterEffect writerEffect;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private playerController playerController;
    private PlayerData playerData;

    private void Start()
    {
        CloseDialogue();
        ShowDialogue(testDialogue);
        playerData = playerController.playerData;
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue)
        {
            yield return writerEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(playerData.nextDialogue));
        }

        CloseDialogue();
    }

    private void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        textLabel.text = "";
    }
}
