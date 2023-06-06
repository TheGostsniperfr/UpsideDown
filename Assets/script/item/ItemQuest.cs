using Mirror;
using UnityEngine;

public class ItemQuest : interactiveInterfaceObject
{
    [SerializeField] private string description = "take the item";
    [SerializeField] private DialogueObject dialogueToShow;

    [Header("Id is either 0,1,2 or 3")]
    [SerializeField] private int IdOfItem;

    public override string getDescription()
    {
        return description;
    }

    public override KeyCode getKey()
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(ActionForKeys.useKey));

    }

    public override void onAction()
    {
        //update the questLevel
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        switch (IdOfItem)
        {
            case 0:
                gameManager.currentItemQuest0 = 1;
                break;
            case 1:
                gameManager.currentItemQuest1 = 1;
                break;
            case 2:
                gameManager.currentItemQuest2 = 1;
                break;
            case 3:
                gameManager.currentItemQuest3 = 1;
                break;

            default:
                break;
        }


        //show dialogue
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            showDialogueToAll(player);
        }


        //Destroy the item
        CmdDestroy(this.gameObject);



    }

    [Command(requiresAuthority = false)]

    private void CmdDestroy(GameObject gameObject)
    {
        NetworkServer.Destroy(gameObject);
    }

    private void showDialogueToAll(GameObject player)
    {
        player.GetComponent<Player>().showDialogue(dialogueToShow);
    }

}
