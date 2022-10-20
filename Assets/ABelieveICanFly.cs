using UnityEngine;

public class ABelieveICanFly : MonoBehaviour
{
    [SerializeField] float amplitude = 1f;
    [SerializeField] float speed = 1f;

    private Vector3 newPos;

    private void Start()
    {
        newPos = transform.localPosition;
    }

    private void FixedUpdate()
    {
        newPos.y = amplitude * Mathf.Sin(Time.realtimeSinceStartup * speed) * amplitude + 1.35f;     

        transform.localPosition = newPos;
    }

}
