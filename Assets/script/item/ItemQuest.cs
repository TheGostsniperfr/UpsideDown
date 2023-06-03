using Mirror;
using UnityEngine;

public class ItemQuest : interactiveInterfaceObject
{
    [SerializeField] private string description = "take the item";

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

        if (IdOfItem >= gameManager.currentQuestLevel.Count) { Debug.LogError("RTFM"); }

        gameManager.currentQuestLevel[IdOfItem] = true;

        //Destroy the item
        CmdDestroy(this.gameObject);

    }

    [Command(requiresAuthority = false)]

    private void CmdDestroy(GameObject gameObject)
    {
        NetworkServer.Destroy(this.gameObject);
    }

}
