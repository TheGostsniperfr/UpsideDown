using Mirror;

public class NetworkSync : NetworkBehaviour
{
    [SyncVar]
    public int gameLevel;

    [Command(requiresAuthority = false)]
    public void CmdUpdateScore(int newGameLevel)
    {
        gameLevel = newGameLevel;
    }
}
