using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliciaEstat: MonoBehaviour
{
	public GameObject player;
    public Camera cam2d;
    public GameObject enemyIdle;
    public GameObject enemyRun;
    public GameObject enemyAttack;
    public GameObject enemyTakingDamage;
    public GameObject enemyDeath;
    public float timerAttack;
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
            if (Vector3.Distance(this.transform.position, player.transform.position) > 1 && Vector3.Distance(this.transform.position, player.transform.position) < 8)
            {
                Vector3 move = (player.transform.position);
                this.transform.position += ((move - transform.position).normalized * Time.deltaTime * speed);
                enemyAttack.SetActive(false);
                enemyIdle.SetActive(false);
                enemyRun.SetActive(true);
            }
            else if (Vector3.Distance(this.transform.position, player.transform.position) >= 8)
            {
                enemyRun.SetActive(false);
                enemyIdle.SetActive(true);
            }
            else
            {
                timerAttack -= Time.deltaTime;
                if(timerAttack <= 0)
                {
                    player.GetComponent<playerController>().damage = true;
                    timerAttack = 2;
                }
                if (!Input.GetKey(KeyCode.Mouse0) && player.GetComponent<playerController>().damage == false)
                {
                    enemyTakingDamage.SetActive(false);
                    enemyRun.SetActive(false);
                    enemyAttack.SetActive(true);
                }
                else if(player.GetComponent<playerController>().damage == false)
                {
                    enemyRun.SetActive(false);
                    enemyAttack.SetActive(false);
                    enemyTakingDamage.SetActive(true);
                    vida = 0;
                }
            }
        }
        else
        {
            enemyIdle.SetActive(false);
            enemyTakingDamage.SetActive(false);
            enemyRun.SetActive(false);
            enemyAttack.SetActive(false);
            enemyDeath.SetActive(true);
        }
    }
}
