using UnityEngine;

public class KeyBindingManager : MonoBehaviour
{
    [SerializeField] private KeyContentControler[] keyContents;
    [SerializeField] private PauseMenu pauseMenu;
    private Player player;


    private void Start()
    {
        player = pauseMenu.player;
    }

    public void showKey()
    {
        PlayerData playerData = player.getPlayerData();

        keyContents[0].setKey("Use", playerData.useKey);
        keyContents[1].setKey("Switch gravity", playerData.switchGravityKey);
        keyContents[2].setKey("test Key", playerData.testKey);

    }


    //save edit keys
    public void save()
    {
        PlayerData playerData = player.getPlayerData();

        Debug.Log("key bind save : " + keyContents[2].getKeyBind() + playerData.testKey);


        //check if the keyBindind has been changed
        if (keyContents[0].getKeyBind() != playerData.useKey) { playerData.useKey = keyContents[0].getKeyBind(); }
        if (keyContents[1].getKeyBind() != playerData.switchGravityKey) { playerData.switchGravityKey = keyContents[1].getKeyBind(); }
        if (keyContents[2].getKeyBind() != playerData.testKey) { playerData.testKey = keyContents[2].getKeyBind(); }


        JSONSaving.saveData(playerData);

        Debug.Log("re load player data");
        player.reLoadPlayerSettings();
    }
}
