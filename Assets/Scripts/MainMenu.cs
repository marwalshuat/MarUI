using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainmenu;
    public GameObject optionsmenu;
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

}
