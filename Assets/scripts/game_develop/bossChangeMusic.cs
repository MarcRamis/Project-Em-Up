using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossChangeMusic : MonoBehaviour
{
    public GameObject player;
    public GameObject music;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) < 10)
        {
            if (music.GetComponent<AudioSource>().volume > 0)
            {
                music.GetComponent<AudioSource>().volume -= 0.01f;
            }
            else
            {
                music.GetComponent<AudioSource>().enabled = false;
                music.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
