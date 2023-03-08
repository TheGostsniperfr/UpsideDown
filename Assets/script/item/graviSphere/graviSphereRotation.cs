using UnityEngine;

public class graviSphereRotation : MonoBehaviour
{
    [SerializeField] private GameObject ring1;
    [SerializeField] private GameObject ring4;


    [SerializeField] private Quaternion newPos1;
    private float speedX1 = 1f;    
    private float speedY1 = 1f;    
    private float speedZ1 = 1f;

    [SerializeField] private Quaternion newPos4;
    private float speedX4 = 1f;
    private float speedY4 = 1f;
    private float speedZ4 = 1f;



    private void Start()
    {
        newPos1 = ring1.gameObject.transform.localRotation;
        newPos4 = ring4.gameObject.transform.localRotation;
        newPos4.w = 0.2f;

    }

    private void FixedUpdate()
    {
        var t1 = Mathf.Sin(Time.timeSinceLevelLoad);
        var t4 = Mathf.Sin(Time.timeSinceLevelLoad + 3);
        var t3 = Mathf.Sin(Time.timeSinceLevelLoad + 7);


        if (t1 <= 0.01f && t1 >= -0.01f)
        {
            speedX1 = Random.Range(0.2f, 2);
            speedY1 = Random.Range(0.2f, 2);
            speedZ1 = Random.Range(0.2f, 2);
        }

        if (t4 <= 0.01f && t4 >= -0.01f)
        {
            speedX4 = Random.Range(0.2f, 2);
            speedY4 = Random.Range(0.2f, 2);
            speedZ4 = Random.Range(0.2f, 2);
        }

        newPos1.x = t1* speedX1;
        newPos1.y = t1* speedY1; 
        newPos1.z = t1* speedZ1;

        newPos4.x = t4 * speedX4;
        newPos4.y = t4 * speedY4;
        newPos4.z = t4 * speedZ4;


        ring1.gameObject.transform.localRotation = newPos1;
        ring4.gameObject.transform.localRotation = newPos4;

  
    }



}
