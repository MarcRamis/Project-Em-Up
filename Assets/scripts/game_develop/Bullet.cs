using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Camera Cam2d;
    public GameObject player;

    //Contador de temps per disparar
    public float shootTimer;

    //Bullet components
    public GameObject bullet;
    public GameObject bulletPrefab;
    public GameObject bulletAux;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 2.0f;
        shootTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= 3.0f && Vector3.Distance(this.transform.position, player.transform.position) > 1)
        {
            bulletAux = Instantiate(bulletPrefab, bullet.transform.position, bullet.transform.rotation); //Crear bala

            bulletAux.transform.position += ((player.transform.position - transform.position).normalized * Time.deltaTime * bulletSpeed);
        }
        
        if (Cam2d.WorldToScreenPoint(this.transform.position).x < 20 || Cam2d.WorldToScreenPoint(this.transform.position).x > 1300)
        {
            Destroy(bulletAux);
        }
        
        //Resetetjar el timer
        if (shootTimer >= 6.0f) shootTimer = 0.0f;
    }
}
