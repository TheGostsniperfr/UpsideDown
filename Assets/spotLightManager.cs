using UnityEngine;

public class spotLightManager : MonoBehaviour
{
    [SerializeField] private GameObject spotlightBase;
    [SerializeField] private GameObject spotlight;

    [SerializeField] private float speedSpotightBase = 1f;
    [SerializeField] private float speedSpotight = 1f;

    [SerializeField] private Vector2 angleSpotlightBase = new Vector2(0f, 360f);
    [SerializeField] private Vector2 angleSpotlight = new Vector2(0f, 360f);

    private Vector2 rdAngleSpotlightBase;
    private Vector2 rdAngleSpotlight;

    [SerializeField] private float refDelay = 3f;

    private float delaySpotlightBase = 3f;
    private float delaySpotlight = 3f;


    private float timeSpotlightBase;
    private float timeSpotlight;



    //generate a random angle to turn the object
    private void GenNewAngle(ref Vector2 rdAngle, Vector2 limitAngle)
    {
        rdAngle.x = rdAngle.y;
        rdAngle.y = Random.Range(limitAngle.x, limitAngle.y);
    }


    private void Start()
    {
        timeSpotlightBase = Time.timeSinceLevelLoad;
        timeSpotlight = Time.timeSinceLevelLoad;


        GenNewAngle(ref rdAngleSpotlightBase, angleSpotlightBase);
        GenNewAngle(ref rdAngleSpotlight, angleSpotlight);

    }

    private void GenNewRdDelay(ref float delay)
    {
        delay = Random.Range(refDelay - refDelay / 3, refDelay + refDelay / 3);
    }


    private void checkTime()
    {
        if (timeSpotlightBase + delaySpotlightBase < Time.timeSinceLevelLoad)
        {
            timeSpotlightBase = Time.timeSinceLevelLoad;
            GenNewAngle(ref rdAngleSpotlightBase, angleSpotlightBase);
            GenNewRdDelay(ref delaySpotlightBase);
        }

        if (timeSpotlight + delaySpotlight < Time.timeSinceLevelLoad)
        {
            timeSpotlight = Time.timeSinceLevelLoad;
            GenNewAngle(ref rdAngleSpotlight, angleSpotlight);
            GenNewRdDelay(ref delaySpotlight);
        }
    }

    private void Update()
    {
        checkTime();


        Vector3 vectSpotlightBase = spotlightBase.transform.localEulerAngles;
        Vector3 vectSpotlight = spotlight.transform.localEulerAngles;

        vectSpotlightBase.y = Mathf.Lerp(rdAngleSpotlightBase.x, rdAngleSpotlightBase.y, (Time.timeSinceLevelLoad - timeSpotlightBase) / delaySpotlightBase);
        vectSpotlight.x = Mathf.Lerp(rdAngleSpotlight.x, rdAngleSpotlight.y, (Time.timeSinceLevelLoad - timeSpotlight) / delaySpotlight);

        spotlightBase.transform.localEulerAngles = vectSpotlightBase;
        spotlight.transform.localEulerAngles = vectSpotlight;
    }
}
