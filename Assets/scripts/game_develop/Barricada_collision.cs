using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricada_collision : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) < 1)
        {
            player.GetComponent<playerController>().move = false;
            if (Input.GetKey(KeyCode.D))
            {
                player.GetComponent<Rigidbody>().velocity = player.GetComponent<playerController>().speed * new Vector3(-5, 0, 0);
                player.GetComponent<playerController>().D = false;
                player.GetComponent<playerController>().A = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                player.GetComponent<Rigidbody>().velocity = player.GetComponent<playerController>().speed * new Vector3(5, 0, 0);
                player.GetComponent<playerController>().D = true;
                player.GetComponent<playerController>().A = false;
            }
            if (Input.GetKey(KeyCode.W))
            {
                this.GetComponent<Rigidbody>().velocity = player.GetComponent<playerController>().speed * new Vector3(0, -5, -5);
                player.GetComponent<playerController>().W = false;
                player.GetComponent<playerController>().S = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.GetComponent<Rigidbody>().velocity = player.GetComponent<playerController>().speed * new Vector3(0, 5, 5);
                player.GetComponent<playerController>().W = true;
                player.GetComponent<playerController>().S = false;
            }

        }
        else
        {
            player.GetComponent<playerController>().move = true;
            player.GetComponent<playerController>().W = true;
            player.GetComponent<playerController>().A = true;
            player.GetComponent<playerController>().S = true;
            player.GetComponent<playerController>().D = true;
        }
    }
}