using UnityEngine;

public class HelpUseKey : interactiveInterfaceObject
{
    public string description = "to use this object";
    [SerializeField] private string keyForAction = "useKey";

    public override string getDescription()
    {
        return description;
    }

    public override KeyCode getKey()
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keyForAction));

    }

    public override void onAction()
    {

    }
}
