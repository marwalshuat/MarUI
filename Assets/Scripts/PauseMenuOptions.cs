using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseOptions : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseOptionsMenu;
    [SerializeField] private GameObject pauseControlsMenu;
    [SerializeField] private GameObject alertBox;

    [SerializeField] private Button controlsButton;
    [SerializeField] private Button optionsBackButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sfxVolume;
    [SerializeField] private AudioMixer mixer;

    // The function for the Master Slider
    public void ChangeMasterVolume(float masterVolume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
    }

    // The function for the sfx slider
    public void ChangeSFXVolume(float sfxVolume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        //Save();
    }

    // The function for the music slider
    public void ChangeMusicVolume(float musicVolume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        //Save();
    }

    // The function that loads the previous settings
    public void LoadOptions()
    {
        float mixerMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        mixer.SetFloat("MasterVolume", mixerMasterVolume);
        float masterSliderValue = PlayerPrefs.GetFloat("MasterVolumeSlider", 1f);
        masterVolume.value = masterSliderValue;
        float mixerMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        mixer.SetFloat("MusicVolume", mixerMusicVolume);
        float musicSliderValue = PlayerPrefs.GetFloat("MusicVolumeSlider", 1f);
        musicVolume.value = musicSliderValue;
        float mixerSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);
        mixer.SetFloat("SFXVolume", mixerSFXVolume);
        float sfxSliderValue = PlayerPrefs.GetFloat("SFXVolumeSlider", 1f);
        sfxVolume.value = sfxSliderValue;
    }

    // The function that save the current settings
    public void SaveOptions()
    {
        float mixerMasterVolume;
        float mixerMusicVolume;
        float mixerSFXVolume;
        mixer.GetFloat("MasterVolume", out mixerMasterVolume);
        mixer.GetFloat("MusicVolume", out mixerMusicVolume);
        mixer.GetFloat("SFXVolume", out mixerSFXVolume);
        PlayerPrefs.SetFloat("MasterVolume", mixerMasterVolume);
        PlayerPrefs.SetFloat("MasterVolumeSlider", masterVolume.value);
        PlayerPrefs.SetFloat("MusicVolume", mixerMusicVolume);
        PlayerPrefs.SetFloat("MusicVolumeSlider", musicVolume.value);
        PlayerPrefs.SetFloat("SFXVolume", mixerSFXVolume);
        PlayerPrefs.SetFloat("SFXVolumeSlider", sfxVolume.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        // The if statement that checks if there is a previous option settings
        if (!PlayerPrefs.HasKey("MusicVolume") && !PlayerPrefs.HasKey("SFXVolume") && !PlayerPrefs.HasKey("MasterVolume"))
        {
            PlayerPrefs.SetFloat("MasterVolumeSlider", 1f);
            PlayerPrefs.SetFloat("MasterVolume", 0f);
            PlayerPrefs.SetFloat("MusicVolumeSlider", 1f);
            PlayerPrefs.SetFloat("MusicVolume", 0f);
            PlayerPrefs.SetFloat("SFXVolumeSlider", 1f);
            PlayerPrefs.SetFloat("SFXVolume", 0f);
            LoadOptions();
        }
        else
        {
            LoadOptions();
        }

        controlsButton.onClick.AddListener(DisplayControlsMenu);
        optionsBackButton.onClick.AddListener(CheckForChanges);
        saveButton.onClick.AddListener(SaveOptions);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayControlsMenu()
    {
        pauseControlsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        pauseOptionsMenu.SetActive(false);
        alertBox.SetActive(false);
    }

    public void CheckForChanges()
    {
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float actualMasterVolume;
        mixer.GetFloat("MaterVolume", out actualMasterVolume);
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float actualMusicVolume;
        mixer.GetFloat("MusicVolume", out actualMusicVolume);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);
        float actualSFXVolume;
        mixer.GetFloat("SFXVolume", out actualSFXVolume);
        if (Mathf.Approximately(savedMasterVolume, actualMasterVolume) && Mathf.Approximately(savedMusicVolume, actualMusicVolume) && Mathf.Approximately(savedSFXVolume, actualSFXVolume))
        {
            // The values are the same
            CloseOptionsMenu();
        }
        else
        {
            // The Values are different
            ShowAlertBox();
        }
    }

    public void CloseOptionsMenu()
    {
        pauseControlsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        pauseOptionsMenu.SetActive(false);
        alertBox.SetActive(false);
    }

    public void ShowAlertBox()
    {
        pauseOptionsMenu.SetActive(false);
        pauseControlsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        alertBox.SetActive(true);
    }

    public void CloseAlertBox()
    {
        pauseMenu.SetActive(true);
        pauseOptionsMenu.SetActive(false);
        pauseControlsMenu.SetActive(false);
        alertBox.SetActive(false);
    }

    public void CloseControlsMenu()
    {
        pauseControlsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        alertBox.SetActive(false);
        pauseOptionsMenu.SetActive(true);
    }
}
