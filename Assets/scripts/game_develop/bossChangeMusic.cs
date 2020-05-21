using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossChangeMusic : MonoBehaviour
{
    public GameObject player;
    public GameObject bossIntro;
    public GameObject bossMusic;
    public GameObject bossEnable;
    public GameObject music;
    public float bossTimer = 3.2f;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 10)
        {
            if (music.GetComponent<AudioSource>().volume > 0)
            {
                music.GetComponent<AudioSource>().volume -= 0.01f;
            }
            else
            {
                music.GetComponent<AudioSource>().enabled = false;
            }
        }
        if (Vector3.Distance(this.transform.position, player.transform.position) < 4)
        {
            bossTimer -= Time.deltaTime;
            if(bossTimer > 0)
            {
                bossMusic.SetActive(true);
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player.GetComponent<playerController>().move = false;
                bossIntro.SetActive(true);
            }
            else
            {
                player.GetComponent<playerController>().move = true;
                bossIntro.SetActive(false);
                bossEnable.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }
}
