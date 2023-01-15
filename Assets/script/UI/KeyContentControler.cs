using TMPro;
using UnityEngine;


public class KeyContentControler : MonoBehaviour
{
    [SerializeField] private TMP_Text keyAction;
    [SerializeField] private TMP_InputField key;

    public void setKey(string name, KeyCode keyCode)
    {
        keyAction.text = name;

        key.text = ((char)keyCode).ToString();
    }


    public KeyCode getKeyBind()
    {
        return (KeyCode)key.text[0];
    }

}
