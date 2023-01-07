
using UnityEngine;
using TMPro;
using static System.Runtime.CompilerServices.RuntimeHelpers;

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


    public void ShowKeyUI(char key, string text)
    {
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

    public void ShowKeyUI(KeyCode keyCode, string text)
    {
        char key = (char)keyCode;

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

    public void removeKeyUI(char key)
    {
        foreach (var keyPos in keyPositions)
        {
            if(keyPos.isActived && keyPos.currentKey == key)
            {
                keyPos.removeKeyDisplay();
                return;
            }
        }
    }

    public void removeKeyUI(KeyCode keyCode)
    {
        char key = (char)keyCode;

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
