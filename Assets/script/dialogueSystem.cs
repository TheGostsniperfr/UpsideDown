using UnityEngine;
using System.Collections;
using TMPro;

public class dialogueSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] typeWriterEffect writerEffect;

    void Start()
    {
        writerEffect.Run("Salut je suis un test\nQui sert a savoir si le system\nde dialogue fontionne", textLabel);
    }
}
