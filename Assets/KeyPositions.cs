
using UnityEngine;
using TMPro;

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

        ShowKeyUI('F', "Je suis un test !");
        ShowKeyUI('A', "Je suis un autre test !");
        ShowKeyUI('P', "EX : Ouvrir la porte");

        //removeKeyUI('A');

        ShowKeyUI('E', "boire la bierre");
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
}
