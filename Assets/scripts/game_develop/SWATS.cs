﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWATS : MonoBehaviour
{
    //Variables per controlar les físiques del jugador.
    public GameObject player;
    public GameObject enemyIdle;
    public GameObject enemyDamage;
    public GameObject enemyRun;
    public GameObject enemyDeath;
    public GameObject enemyShoot;
    public GameObject enemyAttack;
    public GameObject screen;

    public Camera Cam2d;

    public float speed = 2f;
    public float speedShoot = 0f;
    public float timerDamage = 0.8f;
    public float timerDeath = 3;
    public int health;
    public float timerAttack = 2.0f;

    //Variables per manetjar la bala
    public GameObject bullet;
    public GameObject bulletPrefab;
    public GameObject bulletAux;
    public float shootTimer = 0.0f;

    //Booleans per controlar els estats
    public bool damage;
    public bool playerIsClose;

    public bool isClose;
    public float moveWhenIsClose;

    Vector3 move;

    void Start()
    {
        enemyShoot.SetActive(true);

        moveWhenIsClose = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Guardar les rotacions dins la càmera d'aquest objecte i el jugador
        Vector3 rotVectorEnemy = Cam2d.WorldToScreenPoint(this.transform.position);
        Vector3 rotVectorEnemy2 = Cam2d.WorldToScreenPoint(player.transform.position);

        if (health > 0)
        {
            shootTimer += Time.deltaTime;

            //Direccions respecte a la direcció del jugador 
            if (rotVectorEnemy.x - rotVectorEnemy2.x > 0)
            {
                this.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            if (rotVectorEnemy.x - rotVectorEnemy2.x <= 0)
            {
                this.transform.rotation = new Quaternion(0, 180, 0, 0);
            }

            // Condicions per controlar quan el jugador està a prop o en foraa
            if (Vector3.Distance(this.transform.position, player.transform.position) < 15 
                && Vector3.Distance(this.transform.position, player.transform.position) >= 3.5f)
            {
                enemyRun.SetActive(false);
                enemyIdle.SetActive(false);
                enemyAttack.SetActive(false);
                playerIsClose = false;
                if (shootTimer >= 3.0f)
                {
                    bulletAux = Instantiate(bulletPrefab, bullet.transform.position, bullet.transform.rotation);
                    shootTimer = 0.0f;
                }
            }
            else if(Vector3.Distance(this.transform.position, player.transform.position) < 3.5f)
            {
                playerIsClose = true;
                isClose = true;

                if (moveWhenIsClose >= 1.0f && Vector3.Distance(this.transform.position, player.transform.position) > 1) 
                {
                    enemyRun.SetActive(true);
                    enemyIdle.SetActive(false);
                    move = (player.transform.position);
                    this.transform.position += ((move - transform.position).normalized * Time.deltaTime * speed);
                }
                else if(timerAttack > 0)
                {
                    enemyRun.SetActive(false);
                    enemyIdle.SetActive(true);
                }

                timerAttack -= Time.deltaTime;

                if (timerAttack <= 0)
                {
                    enemyIdle.SetActive(false);
                    enemyDamage.SetActive(false);
                    enemyShoot.SetActive(false);
                    enemyRun.SetActive(false);
                    enemyAttack.SetActive(true);

                    if (player.GetComponent<playerController>().life > 0 
                        && player.GetComponent<playerController>().inmunnity <= 0 
                        && (this.transform.position.y - player.transform.position.y) > -0.18f 
                        && (this.transform.position.y - player.transform.position.y) < 0.18f)
                    {
                        if (player.GetComponent<playerController>().cover == false)
                            player.GetComponent<playerController>().damage = true;

                        if (timerAttack <= 0)
                            timerAttack = 2;
                    }
                    else if (player.GetComponent<playerController>().life > 0)
                    {
                        move = (new Vector3(this.transform.position.x, player.transform.position.y, player.transform.position.z));
                        this.transform.position += ((move - transform.position).normalized * Time.deltaTime * speed);
                    }
                }
                else
                {
                    enemyAttack.SetActive(false);
                }
            }
            if (isClose)
            {
                moveWhenIsClose += Time.deltaTime;
            }

            // Condicions per controlar el dany infringit
            if (Vector3.Distance(this.transform.position, player.transform.position) < 2
                && player.GetComponent<playerController>().hitTimer <= 0
                && Input.GetKeyDown(KeyCode.Mouse0)
                && player.GetComponent<playerController>().damage == false
                && damage == false && (this.transform.position.y - player.transform.position.y) > -0.18f
                && (this.transform.position.y - player.transform.position.y) < 0.18f
                && player.GetComponent<playerController>().cover == false)
            {
                damage = true;
                enemyDamage.SetActive(true);
            }
            else if (damage == false)
            {
                enemyDamage.SetActive(false);
            }
            if (damage == true)
            {
                timerDamage -= Time.deltaTime;
                if (timerDamage <= 0)
                {
                    damage = false;
                    health -= 50;
                    timerDamage = 0.8f;
                }
            }

            // Condicions per controlar les anim es que el jugador està a prop
            if (!playerIsClose && !damage)
            {
                enemyShoot.SetActive(true);
                enemyIdle.SetActive(false);

            }
            else if (playerIsClose && !damage)
            {
                enemyIdle.SetActive(true);
                enemyShoot.SetActive(false);
            }
            else
            {
                enemyIdle.SetActive(false);
                enemyShoot.SetActive(false);
            } 
        }

        else
        {
            timerDeath -= Time.deltaTime;
            enemyIdle.SetActive(false);
            enemyDamage.SetActive(false);
            enemyShoot.SetActive(false);
            enemyDeath.SetActive(true);
            
            if (timerDeath <= 0)
            {
                screen.GetComponent<screen_collision>().enemyCounter -= 1;
                Destroy(this.gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "barreel" && other.GetComponent<Barril>().timeThrow > 0 
            && other.GetComponent<Barril>().taken == false && other.GetComponent<Barril>().destroyItem == false)
        {
            other.GetComponent<Barril>().destroyItem = true;
            health = 0;
        }
    }

}
