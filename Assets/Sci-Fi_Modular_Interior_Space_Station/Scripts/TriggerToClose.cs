using UnityEngine;

public class TriggerToClose : MonoBehaviour
{
    public DoorMovement OpenDoor;
    [SerializeField] private string[] tagsToDetect;


    private void OnTriggerExit(Collider col)
    {
        foreach (var tag in tagsToDetect)
        {
            if (col.gameObject.tag == tag)
            {
                OpenDoor.startTime = Time.time;
                OpenDoor.TriggerClosingDoor = true;
                OpenDoor.TriggerOpeningDoor = false;
            }
        }
    }
}
