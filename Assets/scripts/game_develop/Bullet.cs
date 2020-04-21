using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Cam2d;
    public GameObject player;

    //Bullet components
    public float bulletSpeed;

    Vector3 bulletDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Cam2d = GameObject.Find("2dcamera");

        this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        bulletDirection = player.transform.position - this.transform.position;
        bulletDirection /= bulletDirection.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();

        if (Cam2d.GetComponent<Camera>().WorldToScreenPoint(this.transform.position).x < -100 || Cam2d.GetComponent<Camera>().WorldToScreenPoint(this.transform.position).x > 1500)
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    void MoveBullet()
    {
        this.transform.position += bulletDirection * (Time.deltaTime * bulletSpeed);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            DestroyBullet();
        }
    }
}
