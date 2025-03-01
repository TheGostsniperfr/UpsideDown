using UnityEngine;

public class ResponceHandler : MonoBehaviour
{
    [SerializeField] private KeyPositions keyPositions;
    [SerializeField] private Player player;
    private KeyCode[] responsesKeys;
    private Responce[] responces;
    [SerializeField] private dialogueSystem dialogueSystem;
    [SerializeField] private PauseMenu pauseMenu;





    public bool isResponceShow = false;
    private int nbResponce = 0;

    private void Start()
    {
        player = pauseMenu.player;

        responsesKeys = new KeyCode[3]
        {
            (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(ActionForKeys.actionKeyreponse1)),
            (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(ActionForKeys.actionKeyreponse2)),
            (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(ActionForKeys.actionKeyreponse3))
        };
    }

    public void showResponce(Responce[] _responces)
    {
        responces = _responces;
        isResponceShow = true;
        nbResponce = responces.Length;

        //check if the number of response isn't higther of 3
        if (nbResponce > 3) { Debug.LogError("number of responces too higher !"); }

        for (int i = 0; i < nbResponce; i++)
        {
            keyPositions.ShowKeyUI(responsesKeys[i], responces[i].ResponceText);
        }
    }

    public void removeResponce()
    {
        isResponceShow = false;

        for (int i = 0; i < nbResponce; i++)
        {
            keyPositions.removeKeyUI(responsesKeys[i]);
        }
    }

    void Update()
    {
        //check responceKey is pressed
        if (isResponceShow)
        {
            for (int i = 0; i < nbResponce; i++)
            {
                if (Input.GetKeyDown(responsesKeys[i]))
                {
                    removeResponce();

                    //switch dialogue
                    dialogueSystem.ShowDialogue(responces[i].DialogueObject);

                }
            }
        }
    }
}
