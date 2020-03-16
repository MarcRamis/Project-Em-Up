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
    public float timerAttack;
	public float timerdeath;
    public int vida;
    public float speed;
	
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
            if (Vector3.Distance(this.transform.position, player.transform.position) > 1 && Vector3.Distance(this.transform.position, player.transform.position) < 15)
            {
                Vector3 move = (player.transform.position);
                this.transform.position += ((move - transform.position).normalized * Time.deltaTime * speed);
                enemyAttack.SetActive(false);
                enemyIdle.SetActive(false);
                enemyRun.SetActive(true);
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
                    timerAttack = 2;
                }
                if (!Input.GetKeyDown(KeyCode.Mouse0) && player.GetComponent<playerController>().damage == false)
                {
                    enemyIdle.SetActive(true);
                    enemyTakingDamage.SetActive(false);
                    enemyRun.SetActive(false);
                    enemyAttack.SetActive(false);
                }
                else if(player.GetComponent<playerController>().damage == false && this.transform.rotation != player.GetComponent<playerController>().playerMove.transform.rotation)
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
				Destroy(this.gameObject);
			}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "barreel" && other.GetComponent<Barril>().timeThrow > 0)
        {
            vida = 0;
        }
    }
}
