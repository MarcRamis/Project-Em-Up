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
    public bool checkNull;
    public GameObject barreelDestroy;

    // Update is called once per frame
    void Update()
    {
        if(destroyItem == false)
        {
            //Arregla un glich que ens passava quan llencaves un barril a prop d'un altre, el llençat es quedava en mode "thrown" y explotava quan un enemic s'apropava.
            if(thrown == true && left == false && right == false && timerDestroy > 0 && !Input.GetKey(KeyCode.Mouse1))
            {
                thrown = false;
                timeThrow = 0;
            }

            //El personatge agafa el barril mantenint botó dret del mouse pero encara no l'ha llençat.
            if (Vector3.Distance(this.transform.position, player.transform.position) < 1.5 && Input.GetKeyDown(KeyCode.Mouse1) 
                && taken == false && player.GetComponent<playerController>().itemTaken == false 
                && timeThrow <= 0 && player.GetComponent<playerController>().damage == false)
            {
                thrown = false;
                taken = true;
                player.GetComponent<playerController>().itemTaken = true;
                player.GetComponent<playerController>().itemTakenGO = this.gameObject;
                timeThrow = 0.2f;
            }

            //El personatge deixa de tenir el barril i el llença.
            else if (!Input.GetKey(KeyCode.Mouse1) && player.GetComponent<playerController>().damage == false 
                && player.GetComponent<playerController>().itemTakenGO == this.gameObject)
            {
                taken = false;
                thrown = true;
                if (player.GetComponent<playerController>().itemTaken == true)
                    this.transform.GetChild(0).gameObject.SetActive(true);
            }

            //Si el personatge rep mal mentres transporta un barril, el deixa caure.
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

            //Transporta la posició del barril cap al personatge.
            if (taken == true)
            {
                this.transform.position = new Vector3(player.transform.position.x, 
                    player.transform.position.y + 1, player.transform.position.z);
            }

            //Aplica el moviment del barril un cop llançat y controla que sigui cap a la dreta o cap a l'esquerra dins d'un temps delimitat per timeThrow.
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

                //Mentres timeThrow sigui major a 0, el barril estarà en moviment.
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

                //Quan timeThrow sigui menor o igual a 0 es pararà.
                else if (timeThrow <= 0)
                {
                    right = false;
                    left = false;
                    player.GetComponent<playerController>().itemTaken = false;
                    this.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }

        //Codi per destruir el barril i eliminar l'sprite de l'escena després d'un temps definit per timerDestroy
        else
        {
            if(checkNull == false)
            {
                player.GetComponent<playerController>().itemTakenGO = null;
                player.GetComponent<playerController>().itemTaken = false;
                checkNull = true;
            }
            //player.GetComponent<playerController>().itemTakenGO = null;
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