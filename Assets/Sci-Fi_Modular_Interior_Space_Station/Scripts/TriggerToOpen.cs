using UnityEngine;

public class TriggerToOpen : MonoBehaviour
{
    public DoorMovement OpenDoor;
    [SerializeField] private string[] tagsToDetect;

    private void OnTriggerEnter(Collider col)
    {
        foreach (var tag in tagsToDetect)
        {
            if (col.gameObject.tag == tag)
            {
                OpenDoor.startTime = Time.time;
                OpenDoor.TriggerOpeningDoor = true;
                OpenDoor.TriggerClosingDoor = false;
            }
        }


    }


}
