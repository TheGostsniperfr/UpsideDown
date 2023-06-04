using UnityEngine;

public class ResetCubeManager : interactiveInterfaceObject
{
    [SerializeField] private string description = "to reset the cube";
    [SerializeField] private GameObject spawnArea;
    [SerializeField] private GameObject cube;

    private void Start()
    {
        if (cube == null) { Debug.LogError("no cube linked"); }
    }

    public override string getDescription()
    {
        return description;
    }

    public override KeyCode getKey()
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(ActionForKeys.useKey));
    }

    public override void onAction()
    {
        //reset velocity of cube
        Rigidbody rb = cube.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        //teleport the cube at the spawnArea
        cube.transform.position = spawnArea.transform.position;
    }


}
