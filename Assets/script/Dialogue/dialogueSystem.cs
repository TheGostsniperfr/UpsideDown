using System.Collections;
using TMPro;
using UnityEngine;

public class dialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] typeWriterEffect writerEffect;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private playerController playerController;
    private PlayerData playerData;
    [SerializeField] private ResponceHandler responceHandler;

    private void Start()
    {
        CloseDialogue();
        ShowDialogue(testDialogue);
        playerData = playerController.getPlayerData();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return writerEffect.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.hasResponses) { break; }

            yield return new WaitUntil(() => Input.GetKeyDown(playerData.nextDialogue));
        }

        if (dialogueObject.hasResponses)
        {
            responceHandler.showResponce(dialogueObject.Responces);
        }
        else
        {
            CloseDialogue();
        }
    }

    private void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        textLabel.text = "";
    }
}
