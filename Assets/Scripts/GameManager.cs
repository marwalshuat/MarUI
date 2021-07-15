using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Events.EventGameStateChanged OnGameStateChanged = new Events.EventGameStateChanged();
    public enum GameState { MAINMENU, PLAYING, PAUSED, OPTIONSMENU, CONTROLSMENU, PAUSEDOPTIONS, ALERTBOX, PAUSEDCONTROLS };
    private GameState currentGameState = GameState.MAINMENU;
    public GameState previousGameState;
    public Button optionsButton;
    public GameState CurrentGameState
    {
        get
        {
            return currentGameState;
        }
        private set
        {
            currentGameState = value;
        }
    }

    public static GameManager Instance;

    public void ChangeGameState(GameState newGameState)
    {
        previousGameState = currentGameState;
        currentGameState = newGameState;

        switch (currentGameState)
        {
            case GameState.MAINMENU:
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
                break;
            case GameState.OPTIONSMENU:
                Time.timeScale = 1f;
                break;
            case GameState.CONTROLSMENU:
                Time.timeScale = 1f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0f;
                break;
            case GameState.PAUSEDOPTIONS:
                Time.timeScale = 0f;
                break;
            case GameState.PAUSEDCONTROLS:
                Time.timeScale = 0f;
                break;
            case GameState.PLAYING:
                Time.timeScale = 1f;
                break;
            default:
                Debug.LogWarning("Unimplemented GameState");
                break;
        }
        OnGameStateChanged.Invoke(newGameState, previousGameState);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting the game");
    }

    public void PauseOptionsMenu()
    {
        ChangeGameState(GameState.PAUSEDOPTIONS);
    }

    public void PauseControlsMenu()
    {
        ChangeGameState(GameState.PAUSEDCONTROLS);
    }

    public void RestartGame()
    {
        ChangeGameState(GameState.MAINMENU);
    }

    public void TogglePause()
    {
        if (currentGameState == GameState.PAUSED)
        {
            ChangeGameState(GameState.PLAYING);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            ChangeGameState(GameState.PAUSED);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Run right before Start()
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogWarning("Attempted to create a second GameManager");
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MainMenu");
        optionsButton.onClick.AddListener(openOptionsMenu);
    }
    public void openOptionsMenu()
    {
        GameManager.Instance.ChangeGameState(GameState.OPTIONSMENU);
    }
    // Update is called once per frame
    void Update()
    {
        if (currentGameState != GameState.MAINMENU)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }
}
