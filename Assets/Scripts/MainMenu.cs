using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainmenu;
    public GameObject optionsmenu;
    public GameObject alertBox;
    public AudioMixer mixer;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public void QuitGame()
    {
        Debug.Log("Quitting the game");
        Application.Quit();
    }

    public void DisplayOptionsMenu()
    {
        optionsmenu.SetActive(true);
        mainmenu.SetActive(false);
    }

    public void CloseOptionMenu()
    {
        mainmenu.SetActive(true);
        optionsmenu.SetActive(false);
    }

    public void StartNewGame()
    {
        //SceneManager.LoadScene("Level");
        SceneManager.LoadScene(1);
    }

    public void ChangeMasterVolume(float masterVolume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
    }

    public void SaveOptions()
    {
        Debug.Log("Saving Options");
        float mixerMasterVolume;
        mixer.GetFloat("MasterVolume", out mixerMasterVolume);
        PlayerPrefs.SetFloat("MasterVolume", mixerMasterVolume);
        PlayerPrefs.SetFloat("MasterVolumeSlider", masterVolumeSlider.value);
    }

    public void LoadOptions()
    {
        Debug.Log("Loading Options");
        float mixerMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        mixer.SetFloat("MasterVolume", mixerMasterVolume);
        float masterSliderValue = PlayerPrefs.GetFloat("MasterVolumeSlider", 1f);
        masterVolumeSlider.value = masterSliderValue;
    }

    public void ChangeMusicVolume(float musicVolume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }

    public void SaveMusicOptions()
    {
        Debug.Log("Saving Options");
        float mixerMusicVolume;
        mixer.GetFloat("MusicVolume", out mixerMusicVolume);
        PlayerPrefs.SetFloat("MusicVolume", mixerMusicVolume);
        PlayerPrefs.SetFloat("MusicVolumeSlider", musicVolumeSlider.value);
    }

    public void LoadMusicOptions()
    {
        Debug.Log("Loading Options");
        float mixerMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        mixer.SetFloat("MusicVolume", mixerMusicVolume);
        float musicSliderValue = PlayerPrefs.GetFloat("MusicVolumeSlider", 1f);
        musicVolumeSlider.value = musicSliderValue;
    }

    public void ChangeSFXVolume(float sfxVolume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }

    public void SaveSFXOptions()
    {
        Debug.Log("Saving Options");
        float mixerSFXVolume;
        mixer.GetFloat("SFXVolume", out mixerSFXVolume);
        PlayerPrefs.SetFloat("SFXVolume", mixerSFXVolume);
        PlayerPrefs.SetFloat("SFXVolumeSlider", sfxVolumeSlider.value);
    }

    public void LoadSFXOptions()
    {
        Debug.Log("Loading Options");
        float mixerSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);
        mixer.SetFloat("SFXVolume", mixerSFXVolume);
        float sfxSliderValue = PlayerPrefs.GetFloat("SFXVolumeSlider", 1f);
        sfxVolumeSlider.value = sfxSliderValue;
    }

    private void Start()
    {
        LoadOptions();  
    }

    public void CheckForChanges()
    {
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float actualMasterVolume;
        mixer.GetFloat("MasterVolume", out actualMasterVolume);

        //TODO: Actually set this information up in code and in editor.
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float actualMusicVolume;
        mixer.GetFloat("MusicVolume", out actualMusicVolume);

        //TODO: Actually set this information up in code and in editor.
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);
        float actualSFXVolume;
        mixer.GetFloat("SFXVolume", out actualSFXVolume);

        if (Mathf.Approximately(savedMasterVolume, actualMasterVolume) &&
            Mathf.Approximately(savedMusicVolume, actualMusicVolume) &&
            Mathf.Approximately(savedSFXVolume, actualSFXVolume)) 
        {
            // The values are the same
            CloseOptionMenu();
        }
        else
        {
            // The values are different
            ShowAlertBox();        
        }
    }

    public void ShowAlertBox()
    {
        optionsmenu.SetActive(false);
        alertBox.SetActive(true);
    }

    public void CloseAlertBox()
    {
        mainmenu.SetActive(true);
        alertBox.SetActive(false);
    }
}
