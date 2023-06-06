
using UnityEngine;

public class KeyPositions : MonoBehaviour
{
    [SerializeField] private keyPos[] keyPositions;

    private void Start()
    {
        //init display
        foreach (var keyPos in keyPositions)
        {
            keyPos.removeKeyDisplay();
        }
    }

    public bool haveKey(KeyCode keyCode)
    {
        string key = keyCode.ToString();

        foreach (var keyPos in keyPositions)
        {
            if (keyPos.currentKey == key)
            {
                return true;
            }
        }
        return false;
    }



    public void ShowKeyUI(KeyCode keyCode, string text)
    {
        string key = keyCode.ToString();

        //check if the key is already use, if all option is used -> no show key
        foreach (var keyPos in keyPositions)
        {
            if (keyPos.keyAlreadyActive(key))
            {
                keyPos.switchKeyAction(text);
                return;
            }

            if (!keyPos.isActived)
            {
                keyPos.setKeyDisplay(key, text);
                return;
            }
        }
    }


    public void removeKeyUI(KeyCode keyCode)
    {
        string key = keyCode.ToString();

        foreach (var keyPos in keyPositions)
        {
            if (keyPos.isActived && keyPos.currentKey == key)
            {
                keyPos.removeKeyDisplay();
                return;
            }
        }
    }



}
