using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWATS : MonoBehaviour
{
    //Variables per controlar les físiques del jugador.
    public GameObject player;
    public GameObject playerIdle;


    public Camera Cam2d;

    public float speed = 2f;
    public float speedShoot = 0f;
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

    Vector3 move;

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
                bullet.transform.rotation = new Quaternion(0, 0, 90, 0); //Això encara no importa perquè la bala es una capsula i per tant té la mateixa forma.

                if (bulletAux == null)
                {
                    right = true;
                    left = false;
                }
            }
                
            if (rotVectorEnemy.x - rotVectorEnemy2.x <= 0)
            {
                this.transform.rotation = new Quaternion(0, 180, 0, 0);
                bullet.transform.rotation = new Quaternion(0, 0, -90, 0); //Això encara no importa perquè la bala es una capsula i per tant té la mateixa forma.

                if (bulletAux == null)
                {
                    right = false;
                    left = true;
                }
            }

            //Colisió de càmera amb les x i de l'objecte amb les y.
            if ( ( Cam2d.WorldToScreenPoint(this.transform.position).x > 20 && Cam2d.WorldToScreenPoint(this.transform.position).x < 1300 ) 
                && ( this.transform.position.y < - 1 && this.transform.position.y > -3 ) )
            {
                this.transform.position = Vector3.Lerp(this.transform.position,
                    new Vector3(this.transform.position.x, player.transform.position.y, player.transform.position.z), Time.deltaTime * speed);

                //Si la distancia entre el jugador i l'enemic es menor al valor indicat, el contador es major o igual que el contador de temps i no s'ha creat cap bala.
                if (Vector3.Distance(this.transform.position, player.transform.position) < 20 && shootTimer >= 3.0f && bulletAux == null)
                {
                    bulletAux = Instantiate(bulletPrefab, bullet.transform.position, bullet.transform.rotation); //Crear bala
                }

                //Si la direcció es left i només s'ha disparat una bala, es dispara una altra.
                if (left == true && bulletAux != null)
                {
                    bulletAux.transform.Translate(new Vector3(-15, 0, 0) * Time.deltaTime);
                }

                else if (right == true && bulletAux != null)
                {
                    bulletAux.transform.Translate(new Vector3(15, 0, 0) * Time.deltaTime);
                }

                if (bulletAux != null && (Cam2d.WorldToScreenPoint(bulletAux.transform.position).x < -200 || Cam2d.WorldToScreenPoint(bulletAux.transform.position).x > 1500) )
                {
                    Destroy(bulletAux);
                }

                //Resetetjar el timer
                if (shootTimer >= 6.0f) shootTimer = 0.0f;

                if (Vector3.Distance(this.transform.position, player.transform.position) < 3 && Input.GetKey(KeyCode.Mouse0) 
                    && player.GetComponent<playerController>().damage == false)
                {

                }

            }

            else if ( (this.transform.position.y > -1 || this.transform.position.y < -3) || 
                (Cam2d.WorldToScreenPoint(bulletAux.transform.position).x < 0 || Cam2d.WorldToScreenPoint(bulletAux.transform.position).x > 1360) )
            {
                this.transform.Translate(new Vector3(0, 0, 0) * Time.deltaTime * speed);
            }
        }
     
    }
    
}
