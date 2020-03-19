using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWATS : MonoBehaviour
{
	public GameObject player;
    public Camera Cam2d;
    public float speed;
    public int health;

    //Variables per detectar quan es mou l'enemic
    public float timerMove = 0f;
    public bool isTimeToMove = false;

    Vector3 move;

    // Update is called once per frame

    void Update()
    {
        Vector3 rotVectorEnemy = Cam2d.WorldToScreenPoint(this.transform.position);
        Vector3 rotVectorEnemy2 = Cam2d.WorldToScreenPoint(player.transform.position);

        if (health > 0)
        {
            //Rotación 
            if (rotVectorEnemy.x - rotVectorEnemy2.x > 0)
                this.transform.rotation = new Quaternion(0, 0, 0, 0);
            if (rotVectorEnemy.x - rotVectorEnemy2.x <= 0)
                this.transform.rotation = new Quaternion(0, 180, 0, 0);

            //Colisió de càmera amb les x i de l'objecte amb les y.
            if ( ( Cam2d.WorldToScreenPoint(this.transform.position).x > 20 && Cam2d.WorldToScreenPoint(this.transform.position).x < 1300 ) 
                && ( this.transform.position.y < - 1 && this.transform.position.y > -3 ) )
            {
                //Es calcula la distància entre el jugador (u objecte que escollim com a jugador) i aquest objecte. 
                //Si es menor que 3 i major que el contador utilitzat fugeix d'ell
                if (Vector3.Distance(this.transform.position, player.transform.position) < 3 )
                {

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
