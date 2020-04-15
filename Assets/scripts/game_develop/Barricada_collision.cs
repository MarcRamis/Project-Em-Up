using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricada_collision : MonoBehaviour
{
    public GameObject player;
    public bool W;
    public bool A;
    public bool S;
    public bool D;

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            //player.GetComponent<playerController>().move = false;
            if ((this.transform.position.x - player.transform.position.x) > 0 && Input.GetKey(KeyCode.D))
            {
                player.GetComponent<playerController>().move = false;
                D = true;
            }
            else if ((this.transform.position.x - player.transform.position.x) < 0 && Input.GetKey(KeyCode.A))
            {
                player.GetComponent<playerController>().move = false;
                A = true;
            }
            if ((this.transform.position.x - player.transform.position.x) > -1 && (this.transform.position.x - player.transform.position.x) < 1 && (this.transform.position.y - player.transform.position.y) < 0 && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                player.GetComponent<playerController>().move = false;
                S = true;
            }
            else if ((this.transform.position.x - player.transform.position.x) > -1 && (this.transform.position.x - player.transform.position.x) < 1 && (this.transform.position.y - player.transform.position.y) > 0 && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                player.GetComponent<playerController>().move = false;
                W = true;
            }

            //diagonal
            else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                player.GetComponent<playerController>().move = false;
                W = true;
                A = true;
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                player.GetComponent<playerController>().move = false;
                W = true;
                D = true;
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                player.GetComponent<playerController>().move = false;
                S = true;
                A = true;
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                player.GetComponent<playerController>().move = false;
                S = true;
                D = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            W = false;
            S = false;
            A = false;
            D = false;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //player.GetComponent<playerController>().move = true;
        }
    }

    private void Update()
    {
        if(W == true)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.005f, player.transform.position.z - 0.005f);
            player.GetComponent<playerController>().move = true;
        }
        else if(S == true)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.005f, player.transform.position.z + 0.005f);
            player.GetComponent<playerController>().move = true;
        }
        if (A == true)
        {
            player.transform.position = new Vector3(player.transform.position.x + 0.02f, player.transform.position.y, player.transform.position.z);
            player.GetComponent<playerController>().move = true;
        }
        else if (D == true)
        {
            player.transform.position = new Vector3(player.transform.position.x - 0.02f, player.transform.position.y, player.transform.position.z);
            player.GetComponent<playerController>().move = true;
        }
    }
}