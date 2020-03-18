using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barril : MonoBehaviour
{
    public bool taken;
    public GameObject player;
    public float timeThrow;
    public float timerDestroy;
    public bool left;
    public bool right;
    public bool destroyItem;
    public bool thrown = false;
    public GameObject barreelDestroy;

    // Update is called once per frame
    void Update()
    {
        if(destroyItem == false)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < 3 && Input.GetKeyDown(KeyCode.Mouse1) 
                && taken == false && player.GetComponent<playerController>().itemTaken == false 
                && timeThrow <= 0 && player.GetComponent<playerController>().damage == false)
            {
                taken = true;
                player.GetComponent<playerController>().itemTaken = true;
                player.GetComponent<playerController>().itemTakenGO = this.gameObject;
                timeThrow = 0.2f;            
            }

            else if (!Input.GetKey(KeyCode.Mouse1) && player.GetComponent<playerController>().damage == false 
                && player.GetComponent<playerController>().itemTakenGO == this.gameObject)
            {
                taken = false;
                thrown = true;
                if (player.GetComponent<playerController>().itemTaken == true)
                    this.transform.GetChild(0).gameObject.SetActive(true);
            }

            else if (player.GetComponent<playerController>().damage == true && taken == true
                && player.GetComponent<playerController>().itemTakenGO == this.gameObject)
            {
                player.GetComponent<playerController>().itemTaken = false;
                this.transform.position = player.transform.position;
                timeThrow = 0;
                left = false;
                right = false;
                thrown = false;
                taken = false;
            }

            if (taken == true)
            {
                this.transform.position = new Vector3(player.transform.position.x, 
                    player.transform.position.y + 1, player.transform.position.z);
            }

            else if (player.GetComponent<playerController>().itemTaken == true 
                && player.GetComponent<playerController>().itemTakenGO == this.gameObject)
            {
                if (thrown == true)
                {
                    if (player.GetComponent<playerController>().playerMove.transform.rotation == new Quaternion(0, 180, 0, 0))
                        right = true;
                    if (player.GetComponent<playerController>().playerMove.transform.rotation == new Quaternion(0, 0, 0, 1))
                        left = true;
                 
                    thrown = false;
                }
                timeThrow -= Time.deltaTime;
                if (timeThrow > 0)
                {
                    //RIGHT
                    if (right)
                        this.transform.position = new Vector3(this.transform.position.x + 0.5f, player.transform.position.y - 0.5f, 
                            player.transform.position.z);
                    //LEFT
                    if (left)
                        this.transform.position = new Vector3(this.transform.position.x - 0.5f, player.transform.position.y - 0.5f, 
                            player.transform.position.z);
                }
                else if (timeThrow <= 0)
                {
                    right = false;
                    left = false;
                    player.GetComponent<playerController>().itemTaken = false;
                    this.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }

        else
        {
            player.GetComponent<playerController>().itemTakenGO = null;
            timerDestroy -= Time.deltaTime;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            barreelDestroy.SetActive(true);
            if (timerDestroy <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
