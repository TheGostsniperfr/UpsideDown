using UnityEngine;

public class openGatePressurisedPlate : MonoBehaviour
{
    public bool playerDetected = false;
    public string[] listOfTagsToDetect;
    public int[] listOfLayersToDetect;


    private void OnTriggerEnter(Collider col)
    {
        for (int i = 0; i < listOfTagsToDetect.Length; i++)
        {
            if (col.gameObject.tag == listOfTagsToDetect[i])
            {
                playerDetected = true;
            }
        }

        for (int j = 0; j < listOfLayersToDetect.Length; j++)
        {
            if (col.gameObject.layer == listOfLayersToDetect[j])
            {
                playerDetected = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        for (int i = 0; i < listOfTagsToDetect.Length; i++)
        {
            if (col.gameObject.tag == listOfTagsToDetect[i])
            {
                playerDetected = false;
            }
        }

        for (int j = 0; j < listOfLayersToDetect.Length; j++)
        {
            if (col.gameObject.layer == listOfLayersToDetect[j])
            {
                playerDetected = false;
            }
        }
    }



}
