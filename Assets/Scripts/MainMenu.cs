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
        alertBox.SetActive(false);
    }

    public void CloseOptionMenu()
    {
        mainmenu.SetActive(true);
        optionsmenu.SetActive(false);
        alertBox.SetActive(false);
    }

        public void CloseMainMenu()
    {
        mainmenu.SetActive(false);
        optionsmenu.SetActive(false);
    }

    public void StartNewGame()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.PLAYING);
        SceneManager.LoadScene("Level");
        mainmenu.SetActive(false);
    }

    public void ChangeMasterVolume(float masterVolume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
    }

    public void ChangeMusicVolume(float musicVolume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }

    public void ChangeSFXVolume(float sfxVolume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }

    public void LoadOptions()
    {
        float mixerMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        mixer.SetFloat("MasterVolume", mixerMasterVolume);
        float masterSliderValue = PlayerPrefs.GetFloat("MasterVolumeSlider", 1f);
        masterVolumeSlider.value = masterSliderValue;
        float mixerMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        mixer.SetFloat("MusicVolume", mixerMusicVolume);
        float musicSliderValue = PlayerPrefs.GetFloat("MusicVolumeSlider", 1f);
        musicVolumeSlider.value = musicSliderValue;
        float mixerSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);
        mixer.SetFloat("SFXVolume", mixerSFXVolume);
        float sfxSliderValue = PlayerPrefs.GetFloat("SFXVolumeSlider", 1f);
        sfxVolumeSlider.value = sfxSliderValue;
    }

    public void SaveOptions()
    {
        float mixerMasterVolume;
        float mixerMusicVolume;
        float mixerSFXVolume;
        mixer.GetFloat("MasterVolume", out mixerMasterVolume);
        mixer.GetFloat("MusicVolume", out mixerMusicVolume);
        mixer.GetFloat("SFXVolume", out mixerSFXVolume);
        PlayerPrefs.SetFloat("MasterVolume", mixerMasterVolume);
        PlayerPrefs.SetFloat("MasterVolumeSlider", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", mixerMusicVolume);
        PlayerPrefs.SetFloat("MusicVolumeSlider", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", mixerSFXVolume);
        PlayerPrefs.SetFloat("SFXVolumeSlider", sfxVolumeSlider.value);
    }

    private void Start()
    {
        mainmenu.SetActive(true);
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
    }

    public void CheckForChanges()
    {
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float actualMasterVolume;
        mixer.GetFloat("MasterVolume", out actualMasterVolume);
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float actualMusicVolume;
        mixer.GetFloat("MusicVolume", out actualMusicVolume);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);
        float actualSFXVolume;
        mixer.GetFloat("SFXVolume", out actualSFXVolume);
        if (Mathf.Approximately(savedMasterVolume, actualMasterVolume) && Mathf.Approximately(savedMusicVolume, actualMusicVolume) && Mathf.Approximately(savedSFXVolume, actualSFXVolume)) 
        {
            // The values are the same
            GameManager.Instance.ChangeGameState(GameManager.Instance.previousGameState);
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
        optionsmenu.SetActive(false);
        alertBox.SetActive(false);
        Debug.Log(GameManager.Instance.previousGameState);
        GameManager.Instance.ChangeGameState(GameManager.Instance.previousGameState);
    }
}
