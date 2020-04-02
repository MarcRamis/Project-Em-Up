using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliciaLocal : MonoBehaviour
{
    public GameObject player;
    public TextMesh lifestext;
    public Camera cam2d;
    public GameObject enemyIdle;
    public GameObject enemyRun;
    public GameObject enemyAttack;
    public GameObject enemyTakingDamage;
    public GameObject enemyDeath;
    public GameObject scene;
    public float timerAttack;
    public float timerdeath;
    public bool enemyCollision;
    public int vida;
    public float speed;
    public float changeSide;

    public GameObject EnemyCollision;
    Vector3 move;
    Vector3 move2;
    public bool enemytakesDamage;
    public float timerDamage;

    // Update is called once per frame
    void Update()
    {
        //Follow character
        Vector3 rotVectorEnemy = cam2d.WorldToScreenPoint(this.transform.position);
        Vector3 rotVectorEnemy2 = cam2d.WorldToScreenPoint(player.transform.position);

        //Activa totes les mecàniques del enemic sempre i quant tingui vida
        if (vida > 0)
        {
            //agafa la rotació de l'enemic i el jugador dintre del marge de la càmera per avaluar a quina direcció deu mirar l'enemic per encarar-se cap el jugador
            if (rotVectorEnemy.x - rotVectorEnemy2.x > 0)
            {
                this.transform.rotation = new Quaternion(0, 0, 0, 0);
                move2 = new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z);
            }
            if (rotVectorEnemy.x - rotVectorEnemy2.x <= 0)
            {
                this.transform.rotation = new Quaternion(0, 180, 0, 0);
                move2 = new Vector3(player.transform.position.x - 1, player.transform.position.y, player.transform.position.z);
            }
            //activa les mecàniques de perseguir al jugador sempre i quant estigui dintre d'un rang
            if (Vector3.Distance(this.transform.position, player.transform.position) > 1.2f && Vector3.Distance(this.transform.position, player.transform.position) < 15)
            {
                //persegueix al jugador en el càs de no col.lisionar amb cap enemic
                if (enemyCollision == false)
                {
                    this.transform.position += ((move2 - transform.position).normalized * Time.deltaTime * speed);
                    enemyAttack.SetActive(false);
                    enemyIdle.SetActive(false);
                    enemyRun.SetActive(true);
                }
                //s'aparta en el càs de col.lisionar amb un enemic
                if (enemyCollision == true)
                {
                    if (enemyTakingDamage.active == true)
                        enemyTakingDamage.SetActive(false);
                    if (rotVectorEnemy.x - rotVectorEnemy2.x > 0)
                        move = (new Vector3(this.transform.position.x - 1000, player.transform.position.y, player.transform.position.z));
                    if (rotVectorEnemy.x - rotVectorEnemy2.x <= 0)
                        move = (new Vector3(this.transform.position.x + 1000, player.transform.position.y, player.transform.position.z));
                    if (EnemyCollision != null && EnemyCollision.tag == "Enemy_Policia_estat")
                        EnemyCollision.GetComponent<PoliciaEstat>().enemyCollision = false;
                    if (EnemyCollision != null && EnemyCollision.tag == "Enemy_policia_local")
                        EnemyCollision.GetComponent<PoliciaLocal>().enemyCollision = false;
                    EnemyCollision.transform.position += ((move - transform.position).normalized * Time.deltaTime * speed * Random.Range(1, 5));
                    enemyAttack.SetActive(false);
                    enemyIdle.SetActive(false);
                    enemyRun.SetActive(true);
                    if (changeSide > 0)
                    {
                        changeSide -= Time.deltaTime;
                    }
                    else
                    {
                        enemyCollision = false;
                    }
                }
                else
                {
                    changeSide = 0.8f;
                }
            }
            //Es para en el càs d'estar molt lluny el jugador, i es queda en espera
            else if (Vector3.Distance(this.transform.position, player.transform.position) >= 15)
            {
                enemyRun.SetActive(false);
                enemyIdle.SetActive(true);
            }
            //aquí s'activa el càs de que estigui molt a prop, això fa que s'activi el mecanisme de infringir mal al jugador o rebre mal
            else
            {
                timerAttack -= Time.deltaTime;
                if (timerAttack <= 0)
                {
                    enemyIdle.SetActive(false);
                    enemyTakingDamage.SetActive(false);
                    enemyRun.SetActive(false);
                    enemyAttack.SetActive(true);
                    if (player.GetComponent<playerController>().inmunnity <= 0 && (this.transform.position.y - player.transform.position.y) > -0.18f && (this.transform.position.y - player.transform.position.y) < 0.18f)
                    {
                        player.GetComponent<playerController>().damage = true;
                        if (timerAttack <= 0)
                            timerAttack = 2;
                    }
                    else if (player.GetComponent<playerController>().life > 0)
                    {
                        move = (new Vector3(this.transform.position.x, player.transform.position.y, player.transform.position.z));
                        this.transform.position += ((move - transform.position).normalized * Time.deltaTime * speed);
                    }
                }
                //aquí es queda a l'espera del cooldown 'timerAttack' per atacar
                if (!Input.GetKeyDown(KeyCode.Mouse0) && player.GetComponent<playerController>().damage == false)
                {
                    enemyIdle.SetActive(true);
                    enemyTakingDamage.SetActive(false);
                    enemyRun.SetActive(false);
                    enemyAttack.SetActive(false);
                }

                //en el càs de que li peguin dintre del rang rebrà mal i se li resta la vida
                else if (player.GetComponent<playerController>().damage == false && this.transform.rotation
                    != player.GetComponent<playerController>().playerMove.transform.rotation
                    && player.GetComponent<playerController>().hitTimer <= 0 
                    && (this.transform.position.y - player.transform.position.y) > -0.18f 
                    && (this.transform.position.y - player.transform.position.y) < 0.18f)
                {
                    enemytakesDamage = true;
                    vida -= 50;
                }

                if (enemytakesDamage == true)
                {
                    if (timerDamage > 0)
                    {
                        timerDamage -= Time.deltaTime;
                        enemyIdle.SetActive(false);
                        enemyRun.SetActive(false);
                        enemyAttack.SetActive(false);
                        enemyTakingDamage.SetActive(true);
                    }

                    else
                    {
                        enemytakesDamage = false;
                        timerDamage = 0.3f;
                    }
                }
            }
        }
        //En el càs de no tenir vida mor
        else
        {
            timerdeath -= Time.deltaTime;
            enemyIdle.SetActive(false);
            enemyTakingDamage.SetActive(false);
            enemyRun.SetActive(false);
            enemyAttack.SetActive(false);
            enemyDeath.SetActive(true);
            if (timerdeath <= 0)
            {
                scene.GetComponent<screen_collision>().enemyCounter -= 1;
                Destroy(this.gameObject);
            }
        }
    }
    //Detecta col.lisions al.lienes
    private void OnTriggerEnter(Collider other)
    {
        //mort per un cop de barril
        if (other.tag == "barreel" && other.GetComponent<Barril>().timeThrow > 0 && other.GetComponent<Barril>().taken == false)
        {
            other.GetComponent<Barril>().destroyItem = true;
            vida = 0;
        }
        //Detecta si està col.lisionant amb un policia estat
        if (other.tag == "Enemy_Policia_estat")
        {
            EnemyCollision = other.gameObject;
            if (EnemyCollision.GetComponent<PoliciaEstat>().vida > 0)
                enemyCollision = true;
        }
        //Detecta si està col.lisionant amb un policia local
        if (other.tag == "Enemy_policia_local")
        {
            EnemyCollision = other.gameObject;
            if (EnemyCollision.GetComponent<PoliciaLocal>().vida > 0)
                enemyCollision = true;
        }
    }
}
