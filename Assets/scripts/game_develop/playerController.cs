using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    // Per UI + Control de càmera
    public Text lifestext;
    public Camera Cam2d;
    public float timerDeath = 5;
    public int life;
    public int vidas;

    // Moviment
    public bool W;
    public bool A;
    public bool S;
    public bool D;
    public float speed = 4;
    public bool move = true;

    // Variable que utilitzam en el script del barril per saber si hem agafat 
    // o no un objecte (en aquest cas només el barril)
    public bool itemTaken;

    // Immunitat
    public float inmunnity = 0.8f;

    // Variables per controlar el cop de puny
    public bool hitting;
    public bool hitAnim;
    public float hitTimer = 0;
    public float hitAnimTimer = 0;
    public float timerdamage;

    // Taking damage + cover
    public bool damage;
    public bool cover;

    // Variables per controlar l'habilitat embestida del jugador.
    public bool lunge = false;
    public float lungeTimer = 0.5f;
    public float lungeCooldown = 0f;

    // Variables per controlar l'ultimate
    public bool ultimateAttack;
    public float ultimateAttackPlus = 0.0f;
    public float ultimateAttackTimer = 3;
    public GameObject ultimateAttackAnim;
    public GameObject ultimateAttackWindow;

    // Animacions
    public GameObject itemTakenGO;
    public GameObject playerIdle;
    public GameObject playerCover;
    public GameObject playerMove;
    public GameObject playerHitNormal;
    public GameObject playerReceivesDamage;
    public GameObject playerDeath;
    public GameObject textGameOver;
    public GameObject cameraShake;

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

		if(!hitting && !damage && life > 0 && !cover) 
		{
            if (lungeCooldown > 0)
            {
                lungeCooldown -= Time.deltaTime; 
            }

            // Comprobam el Time.timeScale perquè no volem que es puguin fer servir habilitats quan hi ha els popup
            // Comprobam que ultimateAttack sigui false perquè no volem que es pugui fer l'embestida al mateix que temps que es fa l'ultimate
            // Comprobam que el cc de l'embestida sigui menor igual a 0 perquè l'embestida té un temporizador
            // Compobam que no es tengui agafat un objecte (itemTaken)
            if (Input.GetKeyDown(KeyCode.LeftShift) && lungeCooldown <= 0 && !ultimateAttack && Time.timeScale != 0 && !itemTaken && !cover)
            {
                lunge = true;
            }
           
            // Utilitzam un float per saber si la barra del POWER UP es major a 100
            // Comprobam el Time.timeScale perquè no volem que es puguin fer servir habilitats quan hi ha els popup
            if (Input.GetKeyDown(KeyCode.R) && ultimateAttackPlus >= 100.0f && lunge == false && Time.timeScale != 0 && !itemTaken)
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
                ultimateAttackPlus = 0.0f;
                ultimateAttackTimer -= Time.deltaTime;
                ultimateAttackAnim.transform.rotation = playerIdle.transform.rotation;

                if (ultimateAttackTimer <= 0)
                {
                    cameraShake.SetActive(false);
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

            // Comprobam el Time.timeScale perquè no volem que es puguin fer servir habilitats quan hi ha els popup
            // Compobam que no es tengui agafat un objecte quan es vulgui cubrir (itemTaken)
            if (Input.GetKey(KeyCode.E) && inmunnity <= 0 && Time.timeScale != 0.0f && !itemTaken && !lunge)
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
            // Torna a la normalitat quant s'acaba el tamps de inmunitat
            else if(playerIdle.GetComponent<SpriteRenderer>().color == Color.green)
            {
                playerIdle.GetComponent<SpriteRenderer>().color = Color.white;
                playerMove.GetComponent<SpriteRenderer>().color = Color.white;
                playerHitNormal.GetComponent<SpriteRenderer>().color = Color.white;
            }
            
            // Comprobam el Time.timeScale perquè no volem que es puguin fer servir habilitats quan hi ha els popup
            if (move == true && lunge == false && ultimateAttack == false && Time.timeScale != 0.0f)
            {
                if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.D))
                {
                    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && this.transform.position.y < 0f && Cam2d.WorldToScreenPoint(this.transform.position).x > 20)
                {
                    this.GetComponent<Rigidbody>().velocity = speed * new Vector3(-1, 0.5f, 0.5f);
                    playerIdle.transform.rotation = new Quaternion(0, 0, 0, 0);
                    playerMove.transform.rotation = new Quaternion(0, 0, 0, 0);
                }

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && this.transform.position.y < 0f && Cam2d.WorldToScreenPoint(this.transform.position).x < 1300)
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
                if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && this.transform.position.y < 0f && W == true)
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
            
            //Animació ---> Movement - Idle
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && move == true && ultimateAttack == false && Time.timeScale != 0.0f)
            {
                playerIdle.SetActive(false);
                playerMove.SetActive(true);
            }
            else if (!ultimateAttack)
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
            }

            if (Input.GetKey(KeyCode.Mouse0) && hitTimer <= 0 && life > 0 && damage == false)
            {
                hitTimer = 0.5f;
                playerHitNormal.transform.rotation = playerMove.transform.rotation;
                playerIdle.SetActive(false);
                playerMove.SetActive(false);
            }
        }

        else if(cover == false)
        {
            timerdamage -= Time.deltaTime;

            // S'activa l'inmunitat del jugador després de l'animació de rebre mal
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
                playerReceivesDamage.SetActive(true);
            }
        }
        else
        {
            playerMove.SetActive(false);
            playerIdle.SetActive(false);
            playerHitNormal.SetActive(false);
            playerReceivesDamage.SetActive(false);
            playerCover.transform.rotation = playerIdle.transform.rotation;
            playerCover.SetActive(true);
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;

            if (!Input.GetKey(KeyCode.E)) 
            {
                damage = false;
                cover = false;
                playerCover.SetActive(false);
                playerIdle.GetComponent<SpriteRenderer>().color = Color.white;
            }

        }

        if(life <= 0)
        {
            ultimateAttackTimer = 3;
            cameraShake.SetActive(false);
            playerIdle.SetActive(false);
            playerMove.SetActive(false);
            playerCover.SetActive(false);
            ultimateAttackWindow.SetActive(false);
            ultimateAttackAnim.SetActive(false);
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
                    timerDeath = 2;
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
        // Compobam que no es tengui agafat un objecte quan vulgui pegar (itemTaken)
        if (Input.GetKeyDown(KeyCode.Mouse0) && hitAnimTimer <= 0 && life > 0 && cover == false && !itemTaken) 
		{
			hitting = true;
            hitAnim = true;
            hitAnimTimer = 0.5f;
		}
		else 
		{
			hitting = false;
		}

        if(hitAnim == true && life > 0 && cover == false && ultimateAttack == false)
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
        else if(hitAnim == true && life <= 0)
        {
            playerIdle.SetActive(false);
            playerMove.SetActive(false);
            playerCover.SetActive(false);
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