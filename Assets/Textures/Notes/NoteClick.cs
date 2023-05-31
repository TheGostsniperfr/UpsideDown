using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteClick : MonoBehaviour
{
    public Image image;
    [SerializeField] private bool isPressed;
    // Start is called before the first frame update
    void Start()
    {
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
            image.enabled = !image.enabled;
    }
}
