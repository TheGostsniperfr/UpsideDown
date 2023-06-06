using Mirror;

public class NetworkSync : NetworkBehaviour
{
    [SyncVar] public int gameLevel;

    [SyncVar] public int itemQuest0;
    [SyncVar] public int itemQuest1;
    [SyncVar] public int itemQuest2;
    [SyncVar] public int itemQuest3;



    [Command(requiresAuthority = false)]
    public void CmdUpdateScore(int newGameLevel)
    {
        gameLevel = newGameLevel;
    }



    [Command(requiresAuthority = false)]
    public void CmdUpdateQuest0(int newQuestLevel)
    {

        itemQuest0 = newQuestLevel;
    }
    [Command(requiresAuthority = false)]
    public void CmdUpdateQuest1(int newQuestLevel)
    {

        itemQuest1 = newQuestLevel;
    }
    [Command(requiresAuthority = false)]
    public void CmdUpdateQuest2(int newQuestLevel)
    {

        itemQuest2 = newQuestLevel;
    }
    [Command(requiresAuthority = false)]
    public void CmdUpdateQuest3(int newQuestLevel)
    {

        itemQuest3 = newQuestLevel;
    }
}
