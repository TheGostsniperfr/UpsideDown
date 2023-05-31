using System.Collections;
using TMPro;
using UnityEngine;

public class dialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] typeWriterEffect writerEffect;
    [SerializeField] private DialogueObject testDialogue;
    private Player player;
    [SerializeField] private ResponceHandler responceHandler;

    public bool dialogueIsPlaying;



    private void Awake()
    {
        CloseDialogue();
    }

    private void Start()
    {
        //ShowDialogue(testDialogue);

        player = gameObject.GetComponentInParent<PauseMenu>().player;

    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {

        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
        dialogueIsPlaying = true;

    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return writerEffect.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.hasResponses) { break; }

            yield return new WaitUntil(() => Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(ActionForKeys.nextDialogue))));
        }

        if (dialogueObject.hasResponses)
        {
            responceHandler.showResponce(dialogueObject.Responces);
        }
        else
        {
            dialogueIsPlaying = false;
            CloseDialogue();
        }
    }

    private void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        textLabel.text = "";
    }
}
