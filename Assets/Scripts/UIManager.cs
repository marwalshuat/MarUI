using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject HUD;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject controlsMenu;
    public GameObject mainMenu;
    public GameObject pauseOptionsMenu;
    public GameObject pauseControlsMenu;
    public GameObject alertBox;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager.GameState newState, GameManager.GameState previousState)
    {
        switch (newState)
        {
            case GameManager.GameState.MAINMENU:
                mainMenu.SetActive(true);
                break;
            case GameManager.GameState.OPTIONSMENU:
                optionsMenu.SetActive(true);
                break;
            case GameManager.GameState.CONTROLSMENU:
                controlsMenu.SetActive(true);
                break;
            case GameManager.GameState.PAUSED:
                pauseMenu.SetActive(true);
                break;
            case GameManager.GameState.PAUSEDOPTIONS:
                pauseOptionsMenu.SetActive(true);
                break;
            case GameManager.GameState.PAUSEDCONTROLS:
                pauseControlsMenu.SetActive(true);
                break;
            case GameManager.GameState.PLAYING:
                HUD.SetActive(true);
                break;
            default:
                Debug.LogWarning("Unimplemented GameState");
                break;
        }
        switch (previousState)
        {
            case GameManager.GameState.MAINMENU:
                mainMenu.SetActive(false);
                break;
            case GameManager.GameState.OPTIONSMENU:
                optionsMenu.SetActive(false);
                break;
            case GameManager.GameState.CONTROLSMENU:
                controlsMenu.SetActive(false);
                break;
            case GameManager.GameState.PAUSED:
                pauseMenu.SetActive(false);
                break;
            case GameManager.GameState.PAUSEDOPTIONS:
                pauseOptionsMenu.SetActive(false);
                break;
            case GameManager.GameState.PAUSEDCONTROLS:
                pauseControlsMenu.SetActive(false);
                break;
            case GameManager.GameState.PLAYING:
                HUD.SetActive(false);
                break;
            default:
                Debug.LogWarning("Unimplemented GameState");
                break;
        }
    }
}