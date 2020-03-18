using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliciaEstat: MonoBehaviour
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

    public GameObject EnemyCollision;
    Vector3 move;

    // Update is called once per frame
    void Update()
    {
        //Follow character
        Vector3 rotVectorEnemy = cam2d.WorldToScreenPoint(this.transform.position);
        Vector3 rotVectorEnemy2 = cam2d.WorldToScreenPoint(player.transform.position);

        if(vida > 0)
        {
            if (rotVectorEnemy.x - rotVectorEnemy2.x > 0)
                this.transform.rotation = new Quaternion(0, 0, 0, 0);
            if (rotVectorEnemy.x - rotVectorEnemy2.x <= 0)
                this.transform.rotation = new Quaternion(0, 180, 0, 0);
            if (Vector3.Distance(this.transform.position, player.transform.position) > 2 && Vector3.Distance(this.transform.position, player.transform.position) < 15)
            {
                if(enemyCollision == false)
                {
                    Vector3 move = (player.transform.position);
                    this.transform.position += ((move - transform.position).normalized * Time.deltaTime * speed);
                    enemyAttack.SetActive(false);
                    enemyIdle.SetActive(false);
                    enemyRun.SetActive(true);
                }
                if(enemyCollision == true)
                {
                    move = (new Vector3(player.transform.position.x + 1000, player.transform.position.y, player.transform.position.z - 500));
                    if(EnemyCollision.tag == "Enemy_Policia_estat")
                    EnemyCollision.GetComponent<PoliciaEstat>().enemyCollision = false;
                    EnemyCollision.transform.position -= ((move - transform.position).normalized * Time.deltaTime * speed*2);
                    enemyAttack.SetActive(false);
                    enemyIdle.SetActive(false);
                    enemyRun.SetActive(true);
                }
            }
            else if (Vector3.Distance(this.transform.position, player.transform.position) >= 15)
            {
                enemyRun.SetActive(false);
                enemyIdle.SetActive(true);
            }
            else
            {
                timerAttack -= Time.deltaTime;
                if(timerAttack <= 0)
                {
                    enemyIdle.SetActive(false);
                    enemyTakingDamage.SetActive(false);
                    enemyRun.SetActive(false);
                    enemyAttack.SetActive(true);
                    player.GetComponent<playerController>().damage = true;
                    if(timerAttack <= -1)
                    timerAttack = 5;
                }
                if (!Input.GetKeyDown(KeyCode.Mouse0) && player.GetComponent<playerController>().damage == false)
                {
                    enemyIdle.SetActive(true);
                    enemyTakingDamage.SetActive(false);
                    enemyRun.SetActive(false);
                    enemyAttack.SetActive(false);
                }
                else if(player.GetComponent<playerController>().damage == false && this.transform.rotation 
                    != player.GetComponent<playerController>().playerMove.transform.rotation 
                    && player.GetComponent<playerController>().hitTimer <= 0)   
                {
                    enemyRun.SetActive(false);
                    enemyAttack.SetActive(false);
                    enemyTakingDamage.SetActive(true);
                    vida -= 30;
                }
            } 
        }
        else
        {
			timerdeath -= Time.deltaTime;
            enemyIdle.SetActive(false);
            enemyTakingDamage.SetActive(false);
            enemyRun.SetActive(false);
            enemyAttack.SetActive(false);
            enemyDeath.SetActive(true);
			if(timerdeath <= 0) 
			{
                scene.GetComponent<screen_collision>().enemyCounter -= 1;
				Destroy(this.gameObject);
			}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "barreel" && other.GetComponent<Barril>().timeThrow > 0 && other.GetComponent<Barril>().taken == false && other.GetComponent<Barril>().destroyItem == false)
        {
            other.GetComponent<Barril>().destroyItem = true;
            vida = 0;
        }
        if(other.tag == "Enemy_Policia_estat")
        {
            EnemyCollision = other.gameObject;
            if(EnemyCollision.GetComponent<PoliciaEstat>().vida > 0)
            enemyCollision = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy_Policia_estat")
        {
            enemyCollision = false;
        }
    }
}
