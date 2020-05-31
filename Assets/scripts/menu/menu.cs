using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject options;

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void startGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void credits()
    {
        SceneManager.LoadScene("credits");
    }

    public void optionsMenu()
    {
        options.SetActive(true);
    }

    public void closeoptionsMenu()
    {
        options.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
