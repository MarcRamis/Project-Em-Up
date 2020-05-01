using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public float speed = 3;
    public float timerdamage;
    public float timerDeath = 5;
    public int life;
	public int vidas;
    public Camera Cam2d;
    public Text lifestext;
	public bool hitting;
    public bool hitAnim;
    public float hitAnimTimer;
    public bool damage;
    public bool itemTaken;
    public bool cover;
    public bool ultimateAttack;
    
    public GameObject itemTakenGO;
	public GameObject playerIdle;
	public GameObject playerMove;
	public GameObject playerHitNormal;
    public GameObject playerReceivesDamage;
    public GameObject playerDeath;
    public GameObject ultimateAttackAnim;
    public GameObject ultimateAttackWindow;
    public GameObject textGameOver;
    public GameObject cameraShake;

    public bool W;
    public bool A;
    public bool S;
    public bool D;

    public bool move = true;

    public float hitTimer = 0;
    public float inmunnity = 0.8f;

    // Variables per controlar l'habilitat embestida del jugador.
    public bool lunge = false;
    public float lungeTimer = 0.5f;
    public float lungeCooldown = 0f;
    public float ultimateAttackCooldown;
    public float ultimateAttackTimer = 3;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitTimer > 0)
            hitTimer -= Time.deltaTime;

        lifestext.text = "x" + vidas;
        playerMovement();
    }

    void playerMovement()
    {
		if(hitting == false && damage == false && life > 0 && cover == false) 
		{
            if (lungeCooldown > 0)
            {
                lungeCooldown -= Time.deltaTime; 
            }

            if (ultimateAttackCooldown > 0)
            {
                ultimateAttackCooldown -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && lungeCooldown <= 0 && ultimateAttack == false)
            {
                lunge = true;
            }

            if(Input.GetKeyDown(KeyCode.Q) && ultimateAttackCooldown <= 0 && lunge == false)
            {
                ultimateAttackWindow.SetActive(true);
                ultimateAttack = true;
            }

            if (lunge)
            {
                lungeCooldown = 10;
                speed = 15;
                lungeTimer -= Time.deltaTime;

                if (playerIdle.transform.rotation == new Quaternion(0, 180, 0, 0))
                {
                    if (Cam2d.WorldToScreenPoint(this.transform.position).x < 1300)
                        this.GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
                    else
                        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

                else
                {
                    if (Cam2d.WorldToScreenPoint(this.transform.position).x > 20)
                     this.GetComponent<Rigidbody>().velocity = new Vector3(-speed, 0, 0);
                    else
                        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }                    

                if (lungeTimer <= 0)
                {
                    speed = 4;
                    lunge = false;
                    lungeTimer = 0.5f;                    
                }
            }

            if (ultimateAttack)
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                playerIdle.SetActive(false);
                playerMove.SetActive(false);
                ultimateAttackAnim.SetActive(true);
                ultimateAttackCooldown = 50;
                ultimateAttackTimer -= Time.deltaTime;
                ultimateAttackAnim.transform.rotation = playerIdle.transform.rotation;

                if (ultimateAttackTimer <= 0)
                {
                    cameraShake.SetActive(false);
                    this.transform.position = new Vector3(this.transform.position.x + 5, this.transform.position.y, this.transform.position.z);
                    ultimateAttack = false;
                    ultimateAttackTimer = 3;
                }
                else if(ultimateAttackTimer <= 2)
                {
                    cameraShake.SetActive(true);
                    ultimateAttackWindow.SetActive(false);
                }
            }
            else
            {
                ultimateAttackAnim.SetActive(false);
            }

            if (Input.GetKey(KeyCode.E) && inmunnity <= 0)
            {
                cover = true;
            }
            //Sistema d'inmunitat que s'activa al respawn o rebre mal
            if(inmunnity > 0)
            {
                inmunnity -= Time.deltaTime;
                playerIdle.GetComponent<SpriteRenderer>().color = Color.green;
                playerMove.GetComponent<SpriteRenderer>().color = Color.green;
                playerHitNormal.GetComponent<SpriteRenderer>().color = Color.green;
            }
            //torna a la normalitat quant s'acaba el tamps de inmunitat
            else if(playerIdle.GetComponent<SpriteRenderer>().color == Color.green || playerIdle.GetComponent<SpriteRenderer>().color == Color.yellow)
            {
                playerIdle.GetComponent<SpriteRenderer>().color = Color.white;
                playerMove.GetComponent<SpriteRenderer>().color = Color.white;
                playerHitNormal.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if(move == true && lunge == false && ultimateAttack == false)
            {
                if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.D))
                {
                    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && this.transform.position.y < -1f && Cam2d.WorldToScreenPoint(this.transform.position).x > 20)
                {
                    this.GetComponent<Rigidbody>().velocity = speed * new Vector3(-1, 0.5f, 0.5f);
                    playerIdle.transform.rotation = new Quaternion(0, 0, 0, 0);
                    playerMove.transform.rotation = new Quaternion(0, 0, 0, 0);
                }

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && this.transform.position.y < -1f && Cam2d.WorldToScreenPoint(this.transform.position).x < 1300)
                {
                    this.GetComponent<Rigidbody>().velocity = speed * new Vector3(1, 0.5f, 0.5f);
                    playerIdle.transform.rotation = new Quaternion(0, 180, 0, 0);
                    playerMove.transform.rotation = new Quaternion(0, 180, 0, 0);
                }

                if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && this.transform.position.y > -3.0f && Cam2d.WorldToScreenPoint(this.transform.position).x > 20)
                {
                    this.GetComponent<Rigidbody>().velocity = speed * new Vector3(-1, -0.5f, -0.5f);
                    playerIdle.transform.rotation = new Quaternion(0, 0, 0, 0);
                    playerMove.transform.rotation = new Quaternion(0, 0, 0, 0);
                }

                if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && this.transform.position.y > -3.0f && Cam2d.WorldToScreenPoint(this.transform.position).x < 1300)
                {
                    this.GetComponent<Rigidbody>().velocity = speed * new Vector3(1, -0.5f, -0.5f);
                    playerIdle.transform.rotation = new Quaternion(0, 180, 0, 0);
                    playerMove.transform.rotation = new Quaternion(0, 180, 0, 0);
                }

                //Moviment cap amunt amb la Key: W
                if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && this.transform.position.y < -1f && W == true)
                {
                    //this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + new Vector3(0, 0.05f, 0.05f));
                    this.GetComponent<Rigidbody>().velocity = speed * new Vector3(0, 0.5f, 0.5f);
                }
                //Moviment cap a l'esquerra amb la Key: A
                if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && Cam2d.WorldToScreenPoint(this.transform.position).x > 20 && A == true)
                {
                    //this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + new Vector3(-0.05f,0,0));
                    this.GetComponent<Rigidbody>().velocity = speed * Vector3.left;
                    //Sprite que fa rotar al player cap a l'esquerra.
                    playerIdle.transform.rotation = new Quaternion(0, 0, 0, 0);
                    playerMove.transform.rotation = new Quaternion(0, 0, 0, 0);
                    playerReceivesDamage.transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                //Moviment cap abaix amb la Key: S
                if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && this.transform.position.y > -3.0f && S == true)
                {
                    this.GetComponent<Rigidbody>().velocity = speed * new Vector3(0, -0.5f, -0.5f);
                } 
                //Moviment cap a la dreta amb la Key: D
                if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && Cam2d.WorldToScreenPoint(this.transform.position).x < 1300 && D == true)
                {
                    this.GetComponent<Rigidbody>().velocity = speed * Vector3.right;
                    //Sprite que fa rotar al player cap a la dreta.
                    playerIdle.transform.rotation = new Quaternion(0, 180, 0, 0);
                    playerMove.transform.rotation = new Quaternion(0, 180, 0, 0);
                    playerReceivesDamage.transform.rotation = new Quaternion(0, 180, 0, 0);
                }
            }
            
            //Animació
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && move == true && ultimateAttack == false)
            {
                playerIdle.SetActive(false);
                playerMove.SetActive(true);
            }
            else if(ultimateAttack == false)
            {
                playerIdle.SetActive(true);
                playerMove.SetActive(false);
            }
		}

        //HIT
		else if(damage == false && cover == false)
		{
            inmunnity -= Time.deltaTime;
            if (inmunnity <= 0)
            {
                playerIdle.SetActive(false);
                playerMove.SetActive(false);
                //playerHitNormal.SetActive(true);
            }

            if (Input.GetKey(KeyCode.Mouse0) && hitTimer <= 0 && inmunnity > 0 && life > 0 && damage == false)
            {
                hitTimer = 0.35f;
                playerHitNormal.transform.rotation = playerMove.transform.rotation;
                playerIdle.SetActive(false);
                playerMove.SetActive(false);
                //playerHitNormal.SetActive(true);
            }
        }
        else if(cover == false)
        {
            timerdamage -= Time.deltaTime;
            //s'activa l'inmunitat al jugador després de l'animació de rebre mal
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
                //playerHitNormal.SetActive(false);
                playerReceivesDamage.SetActive(true);
            }
        }
        else
        {
            playerMove.SetActive(false);
            playerIdle.SetActive(true);
            playerIdle.GetComponent<SpriteRenderer>().color = Color.yellow;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (!Input.GetKey(KeyCode.E)) 
            {
                damage = false;
                cover = false;
                playerIdle.GetComponent<SpriteRenderer>().color = Color.white;
            }

        }
        if(life <= 0)
        {
            //playerDeath
            ultimateAttackTimer = 3;
            cameraShake.SetActive(false);
            playerIdle.SetActive(false);
            playerMove.SetActive(false);
            ultimateAttackWindow.SetActive(false);
            ultimateAttackAnim.SetActive(false);
            //playerHitNormal.SetActive(false);
            playerReceivesDamage.SetActive(false);
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            timerDeath -= Time.deltaTime;
            if(timerDeath <= 0)
            {
                if (vidas > 0)
                {
                    vidas--;
                    playerDeath.SetActive(false);
                    life = 100;
                    timerDeath = 5;
                }
                else
                {
                    SceneManager.LoadScene("Menu");
                }
            }
            else
            {
                lunge = false;
                ultimateAttack = false;
                speed = 4;
                life = 0;
                playerDeath.gameObject.SetActive(true);
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                if(vidas <= 0)
                {
                    textGameOver.SetActive(true);
                }
            }
        }

        //HIT  == CLICK ESQUERRA del mouse. 
        if (Input.GetKeyDown(KeyCode.Mouse0) && hitAnimTimer <= 0 && life > 0 && cover == false) 
		{
			hitting = true;
            hitAnim = true;
            hitAnimTimer = 0.5f;
		}
		else 
		{
			hitting = false;
		}

        if(hitAnim == true && life > 0)
        {
            playerHitNormal.transform.rotation = playerIdle.transform.rotation;
            hitAnimTimer -= Time.deltaTime;
            if (hitAnimTimer <= 0)
            {
                playerHitNormal.SetActive(false);
                hitAnim = false;
            }
            else
            {
                if(damage == false)
                {
                    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    playerHitNormal.SetActive(true);
                    playerIdle.SetActive(false);
                    playerMove.SetActive(false);
                }
                else
                {
                    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    playerHitNormal.SetActive(false);
                    playerIdle.SetActive(false);
                    playerMove.SetActive(false);
                    playerReceivesDamage.SetActive(true);
                }
            }
        }
        else if(hitAnim == true)
        {
            playerIdle.SetActive(false);
            playerMove.SetActive(false);
            playerHitNormal.SetActive(false);
            playerDeath.SetActive(true);
            hitAnimTimer = 0;
            hitAnim = false;
        }
    }

   

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "bullet" && inmunnity <= 0 && cover == false)
        {
            damage = true;
            life -= 20;
        }
        if(other.gameObject.tag == "bullet")
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}