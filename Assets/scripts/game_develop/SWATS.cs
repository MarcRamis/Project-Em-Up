using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWATS : MonoBehaviour
{
	public GameObject player;
    public Camera Cam2d;

    public float speed = 2f;
    public float speedShoot = 0.2f;
    public int health;

    public GameObject bullet;
    public GameObject bulletPrefab;
    public GameObject bulletAux;

    //Variables per detectar quan es mou l'enemic
    public float timerMove = 0f;
    public bool isTimeToMove = false;

    //Direccions
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
            //Direccions respecte a l'enemic 
            if (rotVectorEnemy.x - rotVectorEnemy2.x > 0)
            {
                this.transform.rotation = new Quaternion(0, 0, 0, 0);
                bullet.transform.rotation = new Quaternion(0, 0, 90, 0); //Això encara no importa perquè la bala es una capsula i per tant té la mateixa forma.
                right = false;
                left = true;
            }
                
            if (rotVectorEnemy.x - rotVectorEnemy2.x <= 0)
            {
                this.transform.rotation = new Quaternion(0, 180, 0, 0);
                bullet.transform.rotation = new Quaternion(0, 0, -90, 0); //Això encara no importa perquè la bala es una capsula i per tant té la mateixa forma.
                right = true;
                left = false;
            }

            //Colisió de càmera amb les x i de l'objecte amb les y.
            if ( ( Cam2d.WorldToScreenPoint(this.transform.position).x > 20 && Cam2d.WorldToScreenPoint(this.transform.position).x < 1300 ) 
                && ( this.transform.position.y < - 1 && this.transform.position.y > -3 ) )
            {
                //Es calcula la distància entre el jugador (u objecte que escollim com a jugador) i aquest objecte. 
                //Si es menor que 3 i major que el contador utilitzat fugeix d'ell
                if (Vector3.Distance(this.transform.position, player.transform.position) > 3)
                {
                    if (bulletAux == null)
                        bulletAux = Instantiate(bulletPrefab, bullet.transform.position, bullet.transform.rotation); //Crear bala

                    if (right == true)
                        bulletAux.transform.Translate(Vector3.right * (Time.deltaTime * speedShoot));
                    
                    if (left == true)
                        bulletAux.transform.Translate(Vector3.left * (Time.deltaTime * speedShoot));
                    //this.transform.position -= ((new Vector3(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y, this.transform.position.z)).normalized * Time.deltaTime * speed);
                }
            }
            else if (this.transform.position.y > -1 || this.transform.position.y < -3)
            {
                //this.transform.position += ((new Vector3(3.02f, -2.08f, this.transform.position.z) * Time.deltaTime * speed));
            }
        }
     
    }
    
}
