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
            if (Input.GetKey(KeyCode.D))
            {
                player.GetComponent<playerController>().D = false;
                player.GetComponent<playerController>().A = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                player.GetComponent<playerController>().D = true;
                player.GetComponent<playerController>().A = false;
            }
            if (Input.GetKey(KeyCode.W))
            {
                player.GetComponent<playerController>().W = false;
                player.GetComponent<playerController>().S = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                player.GetComponent<playerController>().W = true;
                player.GetComponent<playerController>().S = false;
            }
        }
        else
        {
            player.GetComponent<playerController>().W = true;
            player.GetComponent<playerController>().A = true;
            player.GetComponent<playerController>().S = true;
            player.GetComponent<playerController>().D = true;
        }
    }
}