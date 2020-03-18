using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Space))
      {
            SceneManager.LoadScene("Intro");
      }
      if (Input.GetKeyDown(KeyCode.Escape))
      {
            Application.Quit();
      }
    }
}
