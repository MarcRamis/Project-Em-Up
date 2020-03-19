using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 2;
    public float timerdamage;
    public float timerDeath = 5;
    public int life;
	public int vidas;
    public Camera Cam2d;
    public TextMesh lifestext;
	public bool hitting;
    public bool damage;
    public bool itemTaken;
    public GameObject itemTakenGO;
	public GameObject playerIdle;
	public GameObject playerMove;
	public GameObject playerHitNormal;
    public GameObject playerReceivesDamage;
    public GameObject playerDeath;

    public float hitTimer = 0;
    public float inmunnity = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitTimer > 0)
            hitTimer -= Time.deltaTime;

        lifestext.text = "LIFES: " + vidas;
        playerMovement();
    }

    void playerMovement()
    {
		if(hitting == false && damage == false && life > 0) 
		{
            if(inmunnity > 0)
            {
                inmunnity -= Time.deltaTime;
                playerIdle.GetComponent<SpriteRenderer>().color = Color.green;
                playerMove.GetComponent<SpriteRenderer>().color = Color.green;
                playerHitNormal.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if(playerIdle.GetComponent<SpriteRenderer>().color == Color.green)
            {
                playerIdle.GetComponent<SpriteRenderer>().color = Color.white;
                playerMove.GetComponent<SpriteRenderer>().color = Color.white;
                playerHitNormal.GetComponent<SpriteRenderer>().color = Color.white;
            }
            //Moviment cap amunt amb la Key: W
            if (Input.GetKey(KeyCode.W) && this.transform.position.y < -1f)
            {
                this.transform.Translate(new Vector3(0, 1, 1.2f) * Time.deltaTime * speed);
            }
            //Moviment cap a l'esquerra amb la Key: A
            if (Input.GetKey(KeyCode.A) && Cam2d.WorldToScreenPoint(this.transform.position).x > 20)
            {
              this.transform.Translate(Vector3.left * Time.deltaTime * speed);
                //Sprite que fa rotar al player cap a l'esquerra.
                playerIdle.transform.rotation = new Quaternion(0, 0, 0, 0);
                playerMove.transform.rotation = new Quaternion(0, 0, 0, 0);
                playerReceivesDamage.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            //Moviment cap abaix amb la Key: S
            if (Input.GetKey(KeyCode.S) && this.transform.position.y > -3.0f)
            {
                this.transform.Translate(new Vector3(0, -1, -1.2f) * Time.deltaTime * speed);

            }
            //Moviment cap a la dreta amb la Key: D
            if (Input.GetKey(KeyCode.D) && Cam2d.WorldToScreenPoint(this.transform.position).x < 1300)
            {
                this.transform.Translate(Vector3.right * Time.deltaTime * speed);
                //Sprite que fa rotar al player cap a la dreta.
                playerIdle.transform.rotation = new Quaternion(0, 180, 0, 0);
                playerMove.transform.rotation = new Quaternion(0, 180, 0, 0);
                playerReceivesDamage.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 3;
            }
            else
            {
                speed = 2;
            }
            
            //Animació
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

        //HIT
		else if(damage == false)
		{
            if (Input.GetKey(KeyCode.Mouse0) && hitTimer <= 0)
            {
                hitTimer = 0.35f;
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
                inmunnity = 0.8f;
                timerdamage = 0.2f;
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
            playerIdle.SetActive(false);
            playerMove.SetActive(false);
            playerHitNormal.SetActive(false);
            playerReceivesDamage.SetActive(false);
            timerDeath -= Time.deltaTime;
            if(timerDeath <= 0)
            {
                vidas--;
                playerDeath.SetActive(false);
                life = 100;
                timerDeath = 5;
            }
            else
            {
                life = 0;
                playerDeath.gameObject.SetActive(true);
            }
        }
        //HIT  == CLICK DRET del mouse. 
        if (Input.GetKey(KeyCode.Mouse0)) 
		{
			hitting = true;
		}
		else 
		{
			hitting = false;
			playerHitNormal.SetActive(false);
		}
        //RUN : SHIFT
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