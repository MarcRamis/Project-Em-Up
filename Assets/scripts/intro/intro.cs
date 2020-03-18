using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    public float time;

    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (time <= -5)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        if(time <= -12)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
