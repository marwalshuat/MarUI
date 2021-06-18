using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Events.EventGameStateChanged OnGameStateChanged = new Events.EventGameStateChanged(;
    public enum GameState { MAINMENU, PLAYING, PAUSED };
    private GameState currentGameState = GameState.MAINMENU;
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
        GameState previousGameState = currentGameState;
        currentGameState = newGameState;

        switch (currentGameState)
        {
            case GameState.MAINMENU:
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
                break;
            case GameState.PAUSED:
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
        }
        else
        {
            ChangeGameState(GameState.PAUSED);
        }
    }

    // Run right before Start()
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Attempted to create a second GameManager");
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState != GameState.MAINMENU)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            }
        }
    }
}
