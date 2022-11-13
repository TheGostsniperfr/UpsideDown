using System;
using UnityEngine;

public class buttonIsPressed : MonoBehaviour
{
    [SerializeField] private bool isPressed;
    [SerializeField] private bool cubeDetected;
    [SerializeField] private float cooldownBeforeActivate = 0.5f;
    private float timeStartActivate;


    private void FixedUpdate()
    {
        if(cubeDetected && !isPressed && timeStartActivate + cooldownBeforeActivate < Time.time)
        {
            isPressed = true;
        }
    }
    //detect wen the cube press then whit a small cooldown
    private void OnTriggerEnter(Collider collider)
    {
        if (!cubeDetected && collider.tag == "cube") 
        {
            Debug.Log("cibe detected");
            cubeDetected = true;

            if (!isPressed)
            {
                timeStartActivate = Time.time;
            }           
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "cube")
        {
            isPressed = false;
            cubeDetected = false;
            timeStartActivate = 0f;
            Debug.Log("The button is no longer pressed");

        }
    }
}
