using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class spotLightManager : MonoBehaviour
{
    [SerializeField] private GameObject spotlightBase;
    [SerializeField] private GameObject spotlight;

    [SerializeField] private Quaternion newPosBase;
    private float speedY = 1f;

    [SerializeField] private Quaternion newPos;
    private float speedX = 1f;

    int rdspotlightBase;
    int rdspotlight;

    private float time1;
    private float time2;
    [SerializeField] private float delay = 5f;



    private void Start()
    {
        rdspotlightBase = Random.Range(0, 10);
        rdspotlight = Random.Range(0, 10);

        newPosBase = spotlightBase.gameObject.transform.localRotation;
        newPos = spotlight.gameObject.transform.localRotation;

        time1 = Time.timeSinceLevelLoad;
        time2 = Time.timeSinceLevelLoad;
    }


    private void FixedUpdate()
    {
        var t1 = Mathf.Sin(Time.timeSinceLevelLoad + rdspotlightBase);
        var t4 = Mathf.Sin(Time.timeSinceLevelLoad + rdspotlight);

        if (t1 <= 0.01f && t1 >= -0.01f)
        {
            speedY = Random.Range(0.2f, 2);
        }
        else if(time1 + delay < Time.timeSinceLevelLoad)
        {
            time1 = Time.timeSinceLevelLoad;
            rdspotlightBase = Random.Range(0, 10);

        }

        if (t4 <= 0.01f && t4 >= -0.01f)
        {
            speedX = Random.Range(0.2f, 2);
        }
        else if(time2 + delay < Time.timeSinceLevelLoad)
        {
            time2 = Time.timeSinceLevelLoad;
            rdspotlight = Random.Range(0, 10);

        }



        newPosBase.y = t1 * speedY;
        newPos.x = t4 * speedX;

        spotlight.gameObject.transform.localRotation = newPos;
        spotlightBase.gameObject.transform.localRotation = newPosBase;       
    }
}
