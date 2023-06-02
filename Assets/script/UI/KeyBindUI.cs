using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyBindUI : MonoBehaviour, IPointerClickHandler
{
    public TMP_InputField inputField;
    public string actionKey = "No Action linked";

    [SerializeField] private KeyCode defaultKey;
    [SerializeField] string keyDescription;
    [SerializeField] private TMP_Text actionKeyText;

    private bool isListeningForInput = false;

    private void Awake()
    {
        inputField.onSelect.AddListener(OnInputFieldSelect);
        actionKeyText.text = keyDescription;

        //check if the key is save
        if (!PlayerPrefs.HasKey(actionKey))
        {
            PlayerPrefs.SetString(actionKey, defaultKey.ToString());
        }

    }

    private void Start()
    {
        
        //show saved key
        inputField.text = PlayerPrefs.GetString(actionKey);
    }

    private void OnInputFieldSelect(string value)
    {
        isListeningForInput = false;
        inputField.text = "New Key";
        inputField.contentType = TMP_InputField.ContentType.Custom;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isListeningForInput = true;
    }

    private void Update()
    {
        if (isListeningForInput)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    isListeningForInput = false;
                    inputField.text = keyCode.ToString();
                    inputField.contentType = TMP_InputField.ContentType.Standard;

                    //Save key
                    PlayerPrefs.SetString(actionKey, keyCode.ToString());
                    break;
                }
            }
        }
    }
}
