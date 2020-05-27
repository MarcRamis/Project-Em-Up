using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject MainOptionsMenu;
    public GameObject optionsMenu;

    public bool resume = false;
    public bool goMenu = false;
    public bool exit = false;
    public bool options = false;
    public bool press = false;
    public bool mousePosition = false;

    // Update is called once per frame
    void Update()
    {
        if (press && goMenu)
            SceneManager.LoadScene("menu");
        else if (press && exit)
            Application.Quit();
        else if (press && resume)
        {
            Time.timeScale = 1;
            press = false;
            MainOptionsMenu.SetActive(true);
            optionsMenu.SetActive(false);
            pauseMenu.GetComponent<pause>().paused = false;
        }
        else if(press && options)
        {
            press = false;
            optionsMenu.SetActive(true);
            MainOptionsMenu.SetActive(false);
        }
    }  
    
    public void click()
    {
        press = true;
    }
}
