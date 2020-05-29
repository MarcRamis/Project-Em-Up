using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 1;
    }

    public void startGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
