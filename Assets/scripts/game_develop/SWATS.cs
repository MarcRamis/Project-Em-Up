using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWATS : MonoBehaviour
{
	public GameObject player;
    public Camera Cam2d;
    public float speed;
    public int health;
    public float timerMove;
    Vector3 move;

    // Update is called once per frame
    
    void Update()
    {
        Vector3 rotVectorEnemy = Cam2d.WorldToScreenPoint(this.transform.position);
        Vector3 rotVectorEnemy2 = Cam2d.WorldToScreenPoint(player.transform.position);

        if (health > 0)
        {
            if ( (Cam2d.WorldToScreenPoint(this.transform.position).x > 20 && Cam2d.WorldToScreenPoint(this.transform.position).x < 1300) )
            {
                if (rotVectorEnemy.x - rotVectorEnemy2.x > 0)
                    this.transform.rotation = new Quaternion(0, 0, 0, 0);
                if (rotVectorEnemy.x - rotVectorEnemy2.x <= 0)
                    this.transform.rotation = new Quaternion(0, 180, 0, 0);

                if (Vector3.Distance(this.transform.position, player.transform.position) < 3 && timerMove > 3f && Cam2d.WorldToScreenPoint(this.transform.position).z < 2)
                {
                    this.transform.position -= ((new Vector3(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y, 
                        this.transform.position.z)).normalized * Time.deltaTime * speed);
                }
            }

        }
     
    }
    
}
