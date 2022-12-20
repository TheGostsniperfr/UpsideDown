using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class keyPos : MonoBehaviour
{
    [SerializeField] private TMP_Text keyLetter;
    [SerializeField] private TMP_Text keyText;

    public char currentKey;
    public bool isActived;

    public void setKeyDisplay(char key, string text)
    {
        isActived = true;
        currentKey = key;
        keyLetter.text = key.ToString();
        keyText.text = text;
    }

    private void Update()
    {
        if(isActived && Input.GetKeyDown((KeyCode)currentKey))
        {
            Debug.Log($"key : {currentKey} is pressed !");
        }
    }

    public void removeKeyDisplay()
    {
        isActived = false;
        keyLetter.text = string.Empty;
        keyText.text = string.Empty;
    }

    
    public bool keyAlreadyActive(char key)
    {
        return (isActived && currentKey == key);
    }

    public void switchKeyAction(string text)
    {
        keyText.text = text;
    }
}
