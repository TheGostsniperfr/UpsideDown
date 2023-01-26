using UnityEngine;

public class groupOfFlameStream : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] FlameStreams;
    public bool isActivate = true;

    [SerializeField] private int cooldown;
    private float lastThrow = 0;


    private void Update()
    {
        if (isActivate && lastThrow + cooldown < Time.timeSinceLevelLoad)
        {
            lastThrow = Time.timeSinceLevelLoad;

            //switch system
            if (FlameStreams.Length > 0)
            {
                if (FlameStreams[0].GetComponent<ParticleSystem>().isPlaying)
                {
                    Debug.Log("stop throwing");
                    stopThrowing();
                }
                else
                {
                    Debug.Log("start throwing");
                    startThrowing();
                }
            }
        }

        if (!isActivate)
        {
            startThrowing();
        }
    }


    private void stopThrowing()
    {
        foreach (var flameStream in FlameStreams)
        {
            flameStream.GetComponent<ParticleSystem>().Stop(true);
        }
    }

    private void startThrowing()
    {
        foreach (var flameStream in FlameStreams)
        {
            flameStream.GetComponent<ParticleSystem>().Play(true);
        }
    }
}
