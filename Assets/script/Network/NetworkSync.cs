using Mirror;
using System.Collections.Generic;

public class NetworkSync : NetworkBehaviour
{
    [SyncVar] public int gameLevel;

    [SyncVar] public List<bool> questLevel;


    private void Start()
    {
        questLevel = new List<bool> { false, false, false, false };
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateScore(int newGameLevel)
    {
        gameLevel = newGameLevel;
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateQuest(int index)
    {

        questLevel[index] = true;
    }
}
