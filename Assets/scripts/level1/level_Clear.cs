using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_Clear : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;
    public GameObject stageClear;
    public float timerChangeLevel;
    public float timerStart = 0.5f;
    public string levelName;

    // Update is called once per frame
    void Update()
    {
        if(timerStart > 0)
        {
            timerStart -= Time.deltaTime;
        }

        if(camera.GetComponent<screen_collision>().enemyCounter <= 0 && timerStart <= 0)
        {
            player.GetComponent<playerController>().move = false;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            timerChangeLevel -= Time.deltaTime;
            stageClear.SetActive(true);
            if(timerChangeLevel <= 0)
            {
                SceneManager.LoadScene(levelName);
            }
        }
    }
}
