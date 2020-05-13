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
    public string levelName;
    
    // Update is called once per frame
    void Update()
    {
        if ((this.transform.position.x - camera.transform.position.x) <= 2)
        {
            player.GetComponent<playerController>().move = false;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            timerChangeLevel -= Time.deltaTime;
            stageClear.SetActive(true);

            if (timerChangeLevel <= 0)
            {
                SceneManager.LoadScene(levelName);
            }
        }
    }
}
