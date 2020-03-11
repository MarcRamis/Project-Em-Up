using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliciaLocal : MonoBehaviour
{
	public GameObject player;
    public float speed;
	
    // Update is called once per frame
    void Update()
    {
        //Screen collision
        /*
        if (this.transform.position.y < -1.5f)
        {
            this.transform.Translate(new Vector3(0, 1, 1.2f) * Time.deltaTime * speed);
        }

        if (Cam2d.WorldToScreenPoint(this.transform.position).x > 20)
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (this.transform.position.y > -3.0f)
        {
            this.transform.Translate(new Vector3(0, -1, -1.2f) * Time.deltaTime * speed);

        }
        if (Cam2d.WorldToScreenPoint(this.transform.position).x < 1050)
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        */
        //Follow character
        if (Vector3.Distance(this.transform.position, player.transform.position) > 1) 
		{
			Vector3 move = (player.transform.position);
			this.transform.position += ((move - transform.position).normalized * Time.deltaTime * speed);
		}
		else 
		{
			//attack
		}
    }
}
