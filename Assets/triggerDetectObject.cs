
using UnityEngine;

public class triggerDetectObject : MonoBehaviour
{
    public bool isDetected = false;

    public string[] listOfTagsToDetect;
    public int[] listOfLayersToDetect;
    public Collider objectDetect;

    private void OnTriggerStay(Collider col)
    {
        foreach (var tag in listOfTagsToDetect)
        {
            if (col.gameObject.tag == tag)
            {
                isDetected = true;
                objectDetect = col;
            }
        }

        foreach (var layer in listOfLayersToDetect)
        {
            if (col.gameObject.layer == layer)
            {
                isDetected = true;
                objectDetect = col;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        foreach (var tag in listOfTagsToDetect)
        {
            if (col.gameObject.tag == tag)
            {
                isDetected = false;
                objectDetect = null;
            }
        }

        foreach (var layer in listOfLayersToDetect)
        {
            if (col.gameObject.layer == layer)
            {
                isDetected = false;
                objectDetect = null;
            }
        }
    }



}
