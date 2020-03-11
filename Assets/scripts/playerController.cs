using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 2;
    public float timerdamage;
    public int life;
    public Camera Cam2d;
	public bool hitting;
    public bool damage;
	public GameObject playerIdle;
	public GameObject playerMove;
	public GameObject playerHitNormal;
    public GameObject playerReceivesDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
    }

    void playerMovement()
    {
		if(hitting == false && damage == false) 
		{
        if (Input.GetKey(KeyCode.W) && this.transform.position.y < -1f)
        {
            this.transform.Translate(new Vector3(0, 1, 1.2f) * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A) && Cam2d.WorldToScreenPoint(this.transform.position).x > 20)
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * speed);
            //SPRITE ROTATION LEFT
            playerIdle.transform.rotation = new Quaternion(0, 0, 0, 0);
            playerMove.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) && this.transform.position.y > -3.0f)
        {
            this.transform.Translate(new Vector3(0, -1, -1.2f) * Time.deltaTime * speed);

        }
        if (Input.GetKey(KeyCode.D) && Cam2d.WorldToScreenPoint(this.transform.position).x < 1440)
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * speed);
            //SPRITE ROTATION RIGHT
            playerIdle.transform.rotation = new Quaternion(0, 180, 0, 0);
            playerMove.transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 3;
        }
        else
        {
            speed = 2;
        }
            
        //ANIMATION
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            playerIdle.SetActive(false);
            playerMove.SetActive(true);
        }
        else
        {
            playerIdle.SetActive(true);
            playerMove.SetActive(false);
        }
		}
		else if(damage == false)
		{
			if(Input.GetKey(KeyCode.Mouse0)) 
			{
				playerHitNormal.transform.rotation = playerMove.transform.rotation;
				playerIdle.SetActive(false);
				playerMove.SetActive(false);
				playerHitNormal.SetActive(true);
			}
		}
        else
        {
            timerdamage -= Time.deltaTime;
            if(timerdamage <= 0)
            {
                life -= 10;
                playerReceivesDamage.SetActive(false);
                timerdamage = 1;
                damage = false;
            }
            else
            {
                playerIdle.SetActive(false);
                playerMove.SetActive(false);
                playerHitNormal.SetActive(false);
                playerReceivesDamage.SetActive(true);
            }
        }
        if(life <= 0)
        {
            //playerDeath
        }
        //HIT 
		if(Input.GetKey(KeyCode.Mouse0)) 
		{
			hitting = true;
		}
		else 
		{
			hitting = false;
			playerHitNormal.SetActive(false);
		}
        //RUN
		if(Input.GetKey(KeyCode.LeftShift)) 
		{
			speed = 4;
		}
		else 
		{
			speed = 2;
		}
    }

}