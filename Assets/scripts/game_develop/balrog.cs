using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balrog : MonoBehaviour
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

    //Habilitat del balrog
    public GameObject habilityParticles;
    public GameObject hability;
    public bool habilityCheck;
    public float timerCheckHability;

    public GameObject EnemyCollision;
    Vector3 move;
    public bool enemytakesDamage;
    public float timerDamage;

    bool assignPoints = true;
    private bool barreelHit;

    //Decideix si utilitzar un atac fort, dèbil o habilitat
    public int randomAttack;
    public float timerHability;

    //activar final nivell
    public GameObject endLevel;

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
                move = new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z);
            }
            if (rotVectorEnemy.x - rotVectorEnemy2.x <= 0)
            {
                this.transform.rotation = new Quaternion(0, 180, 0, 0);
                move = new Vector3(player.transform.position.x - 1, player.transform.position.y, player.transform.position.z);
            }
            //activa les mecàniques de perseguir al jugador sempre i quant estigui dintre d'un rang
            if (Vector3.Distance(this.transform.position, player.transform.position) > 1.5f && Vector3.Distance(this.transform.position, player.transform.position) < 15 && barreelHit == false)
            {
                if (timerCheckHability <= 0 && enemytakesDamage == false)
                {
                    randomAttack = Random.Range(0, 15);
                    timerCheckHability = 3;
                }
                else if(enemytakesDamage == false)
                {
                    timerCheckHability -= Time.deltaTime;
                }
                if(randomAttack >= 13 && habilityCheck == false)
                {
                    habilityCheck = true;
                }
                //persegueix al jugador en el càs de no col.lisionar amb cap enemic
                if (enemyCollision == false && habilityCheck == false)
                {
                    this.transform.position += ((move - transform.position).normalized * Time.deltaTime * speed);
                    enemyAttack.SetActive(false);
                    if (enemytakesDamage == true && timerHability <= 0)
                    {
                        enemyIdle.SetActive(false);
                        enemyRun.SetActive(false);
                    }
                    else
                    {
                        enemyTakingDamage.SetActive(false);
                        enemyIdle.SetActive(false);
                        enemyRun.SetActive(true);
                    }
                }
                //s'aparta en el càs de col.lisionar amb un enemic
                if (enemyCollision == true && habilityCheck == false)
                {
                    if (enemytakesDamage == true)
                    {
                        enemyIdle.SetActive(false);
                        enemyRun.SetActive(false);
                    }
                    else
                    {
                        enemyTakingDamage.SetActive(false);
                        enemyIdle.SetActive(false);
                        enemyRun.SetActive(true);
                    }
                    if (rotVectorEnemy.x - rotVectorEnemy2.x > 0)
                        move = (new Vector3(this.transform.position.x - 1000, this.transform.position.y, this.transform.position.z));
                    if (rotVectorEnemy.x - rotVectorEnemy2.x <= 0)
                        move = (new Vector3(this.transform.position.x + 1000, this.transform.position.y, this.transform.position.z));
                    if (EnemyCollision != null && EnemyCollision.tag == "Enemy_Policia_estat")
                        EnemyCollision.GetComponent<PoliciaEstat>().enemyCollision = false;
                    if (EnemyCollision != null && EnemyCollision.tag == "Enemy_policia_local")
                        EnemyCollision.GetComponent<PoliciaLocal>().enemyCollision = false;
                    if (EnemyCollision != null)
                        EnemyCollision.transform.position += ((move - transform.position).normalized * Time.deltaTime * speed * Random.Range(1, 3));
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
                    if (randomAttack < 8)
                    {
                        enemyIdle.SetActive(false);
                    }
                    else if(timerAttack > -1)
                    enemyIdle.SetActive(true);
                    else
                    enemyIdle.SetActive(false);
                    enemyTakingDamage.SetActive(false);
                    if(timerHability <= 0)
                    enemyRun.SetActive(false);
                    if(randomAttack < 8)
                    enemyAttack.SetActive(true);

                    if (player.GetComponent<playerController>().life > 0
                        && player.GetComponent<playerController>().inmunnity <= 0
                        && (this.transform.position.y - player.transform.position.y) > -0.18f
                        && (this.transform.position.y - player.transform.position.y) < 0.18f)
                    {
                        if (randomAttack < 8)
                            player.GetComponent<playerController>().damage = true;
                        if (timerAttack <= 0)
                        {
                            if(randomAttack >= 8)
                            {
                                if(timerAttack <= -1)
                                enemyAttack.SetActive(true);
                                if (timerAttack <= -2)
                                {
                                    habilityParticles.SetActive(true);
                                    player.GetComponent<playerController>().damage = true;
                                    player.GetComponent<playerController>().life -= 20;
                                    timerAttack = 2;
                                }
                            }
                            else if(randomAttack < 8)
                            {
                                player.GetComponent<playerController>().life -= 5;
                                timerAttack = 2;
                            }
                        }
                    }
                    else if (player.GetComponent<playerController>().life > 0)
                    {
                        move = (new Vector3(this.transform.position.x, player.transform.position.y, player.transform.position.z));
                        this.transform.position += ((move - transform.position).normalized * Time.deltaTime * speed);
                    }
                }
                else
                {
                    randomAttack = Random.Range(0, 15);
                }
                //aquí es queda a l'espera del cooldown 'timerAttack' per atacar
                if (!Input.GetKeyDown(KeyCode.Mouse0) && player.GetComponent<playerController>().damage == false)
                {
                    enemyIdle.SetActive(true);
                    enemyTakingDamage.SetActive(false);
                    if(timerHability <= 0)
                    enemyRun.SetActive(false);
                    enemyAttack.SetActive(false);
                }

                //en el càs de que li peguin dintre del rang rebrà mal i se li resta la vida
                else if (Input.GetKeyDown(KeyCode.Mouse0) && player.GetComponent<playerController>().damage == false
                    && player.GetComponent<playerController>().life > 0
                    && this.transform.rotation != player.GetComponent<playerController>().playerMove.transform.rotation
                    && player.GetComponent<playerController>().hitTimer <= 0
                    && (this.transform.position.y - player.transform.position.y) > -0.58f
                    && (this.transform.position.y - player.transform.position.y) < 0.58f
                    && player.GetComponent<playerController>().cover == false
                    && habilityCheck == false)
                {
                    randomAttack = 0;
                    habilityCheck = false;
                    habilityParticles.SetActive(false);
                    hability.SetActive(false);
                    enemytakesDamage = true;
                    vida -= 10;
                }

                //Rang en el que l'embestida fa mal 
                if (player.GetComponent<playerController>().lunge == true
                    && (this.transform.position.y - player.transform.position.y) > -2
                    && (this.transform.position.y - player.transform.position.y) < 2)
                {
                    randomAttack = 0;
                    habilityCheck = false;
                    habilityParticles.SetActive(false);
                    hability.SetActive(false);
                    enemytakesDamage = true;
                    vida -= 30;
                }

                if (enemytakesDamage == true)
                {
                    if (timerDamage > 0)
                    {
                        habilityParticles.SetActive(false);
                        timerDamage -= Time.deltaTime;
                        enemyIdle.SetActive(false);
                        enemyRun.SetActive(false);
                        enemyAttack.SetActive(false);
                        enemyTakingDamage.SetActive(true);
                    }

                    else
                    {
                        habilityParticles.SetActive(false);
                        barreelHit = false;
                        enemytakesDamage = false;
                        timerDamage = 0.3f;
                    }
                }
            }
            if (player.GetComponent<playerController>().ultimateAttack == true
                && Vector3.Distance(this.transform.position, player.transform.position) <= 13
                && player.GetComponent<playerController>().ultimateAttackTimer <= 1.5f
                && player.GetComponent<playerController>().ultimateAttackTimer > 0)
            {
                randomAttack = 0;
                habilityCheck = false;
                habilityParticles.SetActive(false);
                hability.SetActive(false);
                enemytakesDamage = true;
                vida -= 1;
            }
            if(habilityCheck == true)
            {
                enemyRun.SetActive(false);
                enemyAttack.SetActive(false);
                enemyTakingDamage.SetActive(false);
                timerHability += Time.deltaTime;
                //randomAttack = 15;
                if (timerHability < 3)
                {
                    enemyIdle.SetActive(true);
                    habilityParticles.SetActive(true);
                }
                else if (timerHability < 5)
                {
                    enemyIdle.SetActive(false);
                    hability.SetActive(true);
                    if (Vector3.Distance(player.transform.position, this.transform.position) < 4 && player.GetComponent<playerController>().damage == false && player.GetComponent<playerController>().lunge == false && player.GetComponent<playerController>().inmunnity <= 0)
                    {
                        player.GetComponent<playerController>().life -= 50;
                        player.GetComponent<playerController>().damage = true;
                    }
                }
                else if (timerHability >= 5)
                {
                    hability.SetActive(false);
                    habilityParticles.SetActive(false);
                    timerAttack = 2;
                    timerHability = 0;
                    habilityCheck = false;
                }
            }
            if (enemytakesDamage == true && timerHability > 0 && Vector3.Distance(this.transform.position, player.transform.position) > 1.5f)
            {
                enemyTakingDamage.SetActive(false);
                enemyRun.SetActive(true);
                enemytakesDamage = false;
                timerDamage = 0.3f;
                timerHability = 0;
            }
            if (timerDamage > 0 && Vector3.Distance(this.transform.position, player.transform.position) > 1.5f && enemytakesDamage == true)
            {
                habilityParticles.SetActive(false);
                timerDamage -= Time.deltaTime;
            }
            else if(Vector3.Distance(this.transform.position, player.transform.position) > 1.5f && enemytakesDamage == true)
            {
                habilityParticles.SetActive(false);
                enemyTakingDamage.SetActive(false);
                enemyRun.SetActive(true);
                barreelHit = false;
                enemytakesDamage = false;
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
                endLevel.SetActive(true);
                scene.GetComponent<screen_collision>().enemyCounter -= 1;
                Destroy(this.gameObject);
            }

            if (!player.GetComponent<playerController>().ultimateAttack && assignPoints)
            {
                player.GetComponent<playerController>().ultimateAttackPlus += 5.0f;
                assignPoints = false;
            }
            else if (assignPoints)
            {
                assignPoints = false;
            }
        }
    }
    //Detecta col.lisions al.lienes
    private void OnTriggerEnter(Collider other)
    {
        //cop de barril
        if (other.tag == "barreel" && other.GetComponent<Barril>().timeThrow > 0 && other.GetComponent<Barril>().taken == false && other.GetComponent<Barril>().destroyItem == false && (other.GetComponent<Barril>().right == true || other.GetComponent<Barril>().left == true))
        {
            barreelHit = true;
            randomAttack = 0;
            timerHability = 0;
            habilityCheck = false;
            habilityParticles.SetActive(false);
            hability.SetActive(false);
            enemyTakingDamage.SetActive(true);
            other.GetComponent<Barril>().destroyItem = true;
            enemytakesDamage = true;
            vida -= 50;
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
        if (other.tag == "Enemy_Militar")
        {
            EnemyCollision = other.gameObject;
            if (EnemyCollision.GetComponent<Militar>().vida > 0)
                enemyCollision = true;
        }
    }
}
