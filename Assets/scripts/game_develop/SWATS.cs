using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWATS : MonoBehaviour
{
    //Variables per controlar les físiques del jugador.
    public GameObject player;
    public GameObject enemyIdle;
    public GameObject enemyDamage;
    public GameObject enemyDeath;
    public GameObject enemyShoot;
    public GameObject screen;

    public Camera Cam2d;

    public float speed = 2f;
    public float speedShoot = 0f;
    public float timerDamage = 0.8f;
    public float timerDeath = 3;
    public int health;

    //Variables per manetjar la bala
    public GameObject bullet;
    public GameObject bulletPrefab;
    public GameObject bulletAux;
    public float shootTimer = 0.0f;

    //Variables per detectar quan es mou l'enemic
    public float timerMove = 0f;
    public bool isTimeToMove = false;

    //Booleans per controlar les direccions
    public bool right, left;
    public bool damage;

    Vector3 move;

    void Start()
    {
        enemyShoot.SetActive(true);
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

            //Direccions respecte a l'enemic 
            if (rotVectorEnemy.x - rotVectorEnemy2.x > 0)
            {
                this.transform.rotation = new Quaternion(0, 0, 0, 0);
                bullet.transform.rotation = new Quaternion(0, 0, 90, 0);
            }
                
            if (rotVectorEnemy.x - rotVectorEnemy2.x <= 0)
            {
                this.transform.rotation = new Quaternion(0, 180, 0, 0);
                bullet.transform.rotation = new Quaternion(0, 0, -90, 0); //Això encara no importa perquè la bala es una capsula i per tant té la mateixa forma.
            }

            if (Vector3.Distance(this.transform.position, player.transform.position) >1)
            {
                enemyShoot.SetActive(true);
                enemyIdle.SetActive(false);

            }
            else
            {
                enemyIdle.SetActive(true);
                enemyShoot.SetActive(false);
            }

            //Distància entre SWAT i jugador.
            if (Vector3.Distance(this.transform.position, player.transform.position) < 15 && health > 0)
            {
                if (shootTimer >= 3.0f)
                {
                    bulletAux = Instantiate(bulletPrefab, bullet.transform.position, bullet.transform.rotation);
                    shootTimer = 0.0f;
                }

                if (Vector3.Distance(this.transform.position, player.transform.position) < 1 && player.GetComponent<playerController>().hitTimer <= 0 
                    && Input.GetKeyDown(KeyCode.Mouse0) && player.GetComponent<playerController>().damage == false 
                    && damage == false && (this.transform.position.y - player.transform.position.y) > -0.18f && (this.transform.position.y - player.transform.position.y) < 0.18f)
                {
                    damage = true;
                    enemyDamage.SetActive(true);
                }
                else if (damage == false)
                {
                    enemyDamage.SetActive(false);
                }
                if(damage == true)
                {
                    timerDamage -= Time.deltaTime;
                    if(timerDamage <= 0)
                    {
                        damage = false;
                        health -= 50;
                        timerDamage = 0.8f;
                    }
                }
            }
        }
        else
        {
            timerDeath -= Time.deltaTime;
            enemyIdle.SetActive(false);
            enemyDamage.SetActive(false);
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
        if(other.tag == "barreel" && other.GetComponent<Barril>().timeThrow > 0 && other.GetComponent<Barril>().taken == false && other.GetComponent<Barril>().destroyItem == false && (other.GetComponent<Barril>().right == true || other.GetComponent<Barril>().left == true))
        {
            other.GetComponent<Barril>().destroyItem = true;
            health = 0;
        }
    }

}
