using UnityEngine;
using UnityEngine.SceneManagement;

public class playerSoundManager : MonoBehaviour
{
    [Header("Foot Sound")]
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource runSound;

    [Header("hand Sound")]
    [SerializeField] private AudioSource switchingGragity;
    [SerializeField] private AudioSource grabSomethings;

    [Header("Ambient Sound")]
    [SerializeField] private AudioSource ambientSound;
    [SerializeField] private AudioClip[] ambientMusiks;
    [SerializeField] private AudioClip HubMusic;
    int index = 0;


    private void Start()
    {
        takeRandomAmbientMusik();
    }

    private void Update()
    {
        if (!ambientSound.isPlaying)
        {

            if (SceneManager.GetActiveScene().name == "Hub")
            {
                ambientSound.clip = HubMusic;
            }
            else
            {


                index = (index + 1) % ambientMusiks.Length;

                ambientSound.clip = ambientMusiks[index];
            }
            ambientSound.Play();


        }
    }

    private void takeRandomAmbientMusik()
    {

        if(SceneManager.GetActiveScene().name == "Hub")
        {
            ambientSound.clip = HubMusic;
        }
        else
        {
            index = Random.Range(0, ambientMusiks.Length);
            ambientSound.clip = ambientMusiks[index];
        }

        
    }

    public void isAmbient(bool state)
    {
        if (state && !ambientSound.isPlaying)
        {
            ambientSound.Play();
        }
        else
        {
            ambientSound.Stop();
        }
    }


    public void isWalking(bool state)
    {

        if (state && !walkSound.isPlaying && !runSound.isPlaying)
        {
            walkSound.Play();
        }
        else
        {
            walkSound.Stop();
        }
    }

    public void isRunning(bool state)
    {


        if (state && !runSound.isPlaying)
        {
            runSound.Play();
            walkSound.Stop();
        }
        else
        {
            runSound.Stop();
        }
    }

    public void isSwitchingGragity()
    {
        switchingGragity.Play();
    }

    public void isGrab()
    {
        grabSomethings.Play();
    }

}
