using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class settingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;


    [Header("settingsBars")]
    [SerializeField] private Slider masterVol;
    [SerializeField] private Slider SFXVol;
    [SerializeField] private Slider musicVol;
    [SerializeField] private Slider voiceVol;
    [SerializeField] private TMP_Dropdown qualityIndex;
    [SerializeField] private Slider rorationSpeed;
    [SerializeField] private Slider mouseSensiX;
    [SerializeField] private Slider mouseSensiY;


    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResoltionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResoltionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResoltionIndex;
        resolutionDropdown.RefreshShownValue();

        //set saved configuration :
        //if the PlayerPref doesn't exist, save and load the default configuration

        if (PlayerPrefs.HasKey("masterVol"))
        {
            masterVol.value = PlayerPrefs.GetFloat("masterVol");

            SetMasterVolume(PlayerPrefs.GetFloat("masterVol"));

        }
        else
        {
            masterVol.value = 1;
            SetMasterVolume(1);
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            SFXVol.value = PlayerPrefs.GetFloat("SFXVol");

            SetSFXVolume(PlayerPrefs.GetFloat("SFXVol"));

        }
        else
        {
            SFXVol.value = 1;
            SetSFXVolume(1);
        }
        if (PlayerPrefs.HasKey("musicVol"))
        {
            musicVol.value = PlayerPrefs.GetFloat("musicVol");

            SetMusicVolume(PlayerPrefs.GetFloat("musicVol"));

        }
        else
        {
            musicVol.value = 1;
            SetMusicVolume(1);
        }
        if (PlayerPrefs.HasKey("voiceVol"))
        {
            voiceVol.value = PlayerPrefs.GetFloat("voiceVol");

            SetVoiceVolume(PlayerPrefs.GetFloat("voiceVol"));

        }
        else
        {
            voiceVol.value = 1;
            SetVoiceVolume(1);
        }
        if (PlayerPrefs.HasKey("qualityIndex"))
        {
            qualityIndex.value = PlayerPrefs.GetInt("qualityIndex");

            SetQuality(PlayerPrefs.GetInt("qualityIndex"));

        }
        else
        {
            qualityIndex.value = 0;
            SetQuality(0);
        }
        if (PlayerPrefs.HasKey("rorationSpeed"))
        {
            rorationSpeed.value = PlayerPrefs.GetFloat("rorationSpeed");

            SetRorationSpeed(PlayerPrefs.GetFloat("rorationSpeed"));
        }
        else
        {
            rorationSpeed.value = 1.8f;
            SetRorationSpeed(1.8f);
        }

        if (PlayerPrefs.HasKey("mouseSensiX"))
        {   
            mouseSensiX.value = PlayerPrefs.GetFloat("mouseSensiX");

            SetMouseSensiX(PlayerPrefs.GetFloat("mouseSensiX"));
        }
        else
        {
            mouseSensiX.value = 1;
            SetMouseSensiX(1);
        }

        if (PlayerPrefs.HasKey("mouseSensiY"))
        {
            mouseSensiY.value = PlayerPrefs.GetFloat("mouseSensiY");

            SetMouseSensiY(PlayerPrefs.GetFloat("mouseSensiY"));
        }
        else
        {
            mouseSensiY.value = 1;
            SetMouseSensiY(1);
        }

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVol", Mathf.Log10(volume) * 20);


        PlayerPrefs.SetFloat("masterVol", volume);


    }
    public void SetSFXVolume(float volume)
    {

        audioMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);


        PlayerPrefs.SetFloat("SFXVol", volume);


    }
    public void SetMusicVolume(float volume)
    {

        audioMixer.SetFloat("musicVol", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("musicVol", volume);


    }
    public void SetVoiceVolume(float volume)
    {
       

        audioMixer.SetFloat("voiceVol", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("voiceVol", volume);


    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);


        PlayerPrefs.SetInt("qualityIndex", qualityIndex);


    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetRorationSpeed(float speed)
    {

        PlayerPrefs.SetFloat("rorationSpeed", speed);


    }

    public void SetMouseSensiX(float X)
    {
        PlayerPrefs.SetFloat("mouseSensiX", X);


    }

    public void SetMouseSensiY(float Y)
    {
        PlayerPrefs.SetFloat("mouseSensiY", Y);
    }
}