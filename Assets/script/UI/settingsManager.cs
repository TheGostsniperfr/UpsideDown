using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class settingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private PauseMenu pauseMenu;
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



        if (PlayerPrefs.HasKey("masterVol"))
        {
            masterVol.value = PlayerPrefs.GetFloat("masterVol");

            SetMasterVolume(PlayerPrefs.GetFloat("masterVol"));

        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            SFXVol.value = PlayerPrefs.GetFloat("SFXVol");

            SetSFXVolume(PlayerPrefs.GetFloat("SFXVol"));

        }
        if (PlayerPrefs.HasKey("musicVol"))
        {
            musicVol.value = PlayerPrefs.GetFloat("musicVol");

            SetMusicVolume(PlayerPrefs.GetFloat("musicVol"));

        }
        if (PlayerPrefs.HasKey("voiceVol"))
        {
            voiceVol.value = PlayerPrefs.GetFloat("voiceVol");

            SetVoiceVolume(PlayerPrefs.GetFloat("voiceVol"));

        }
        if (PlayerPrefs.HasKey("qualityIndex"))
        {
            qualityIndex.value = PlayerPrefs.GetInt("qualityIndex");

            SetQuality(PlayerPrefs.GetInt("qualityIndex"));

        }
        if (PlayerPrefs.HasKey("rorationSpeed"))
        {
            rorationSpeed.value = PlayerPrefs.GetFloat("rorationSpeed");

            SetRorationSpeed(PlayerPrefs.GetFloat("rorationSpeed"));
        }

        if (PlayerPrefs.HasKey("mouseSensiX"))
        {
            mouseSensiX.value = PlayerPrefs.GetFloat("mouseSensiX");

            SetMouseSensiX(PlayerPrefs.GetFloat("mouseSensiX"));
        }

        if (PlayerPrefs.HasKey("mouseSensiY"))
        {
            mouseSensiY.value = PlayerPrefs.GetFloat("mouseSensiY");

            SetMouseSensiY(PlayerPrefs.GetFloat("mouseSensiY"));
        }

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMasterVolume(float volume)
    {
        float vol = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("masterVol", vol);


        PlayerPrefs.SetFloat("masterVol", volume);


    }
    public void SetSFXVolume(float volume)
    {
        float vol = Mathf.Log10(volume) * 20;

        audioMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);


        PlayerPrefs.SetFloat("SFXVol", volume);


    }
    public void SetMusicVolume(float volume)
    {
        float vol = Mathf.Log10(volume) * 20;

        audioMixer.SetFloat("musicVol", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("musicVol", volume);


    }
    public void SetVoiceVolume(float volume)
    {
        float vol = Mathf.Log10(volume) * 20;

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
        pauseMenu.playerController.gravitRotationSpeed = speed;

        PlayerPrefs.SetFloat("rorationSpeed", speed);


    }

    public void SetMouseSensiX(float X)
    {
        pauseMenu.playerController.mouseSensitivityX = X;


        PlayerPrefs.SetFloat("mouseSensiX", X);


    }

    public void SetMouseSensiY(float Y)
    {
        pauseMenu.playerController.mouseSensitivityY = Y;


        PlayerPrefs.SetFloat("mouseSensiY", Y);


    }
}