using TMPro;
using UnityEngine;

public class keyPos : MonoBehaviour
{
    [SerializeField] private TMP_Text keyLetter;
    [SerializeField] private TMP_Text keyText;
    [SerializeField] Animation animator;

    public string currentKey;
    public bool isActived;
    [SerializeField] private bool isPlayingAnimation;

    public void setKeyDisplay(string key, string text)
    {
        isActived = true;
        currentKey = key;
        keyLetter.text = key.ToString();
        keyText.text = text;
    }

    private void Update()
    {
        if (isActived && !isPlayingAnimation && Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), currentKey)))
        {
            Debug.Log($"key : {currentKey} is pressed !");
            isPlayingAnimation = true;
        }

        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), currentKey)))
        {
            isPlayingAnimation = false;
        }

        if (isPlayingAnimation)
        {
            //animator.Play("");
        }
    }

    public void removeKeyDisplay()
    {
        isActived = false;
        keyLetter.text = string.Empty;
        keyText.text = string.Empty;
    }


    public bool keyAlreadyActive(string key)
    {
        return (isActived && currentKey == key);
    }

    public void switchKeyAction(string text)
    {
        keyText.text = text;
    }
}
