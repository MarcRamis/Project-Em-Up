using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    public GameObject Cam2d;
    public GameObject player;

    //Granada components
    public float granadaSpeed = 10;
    public float granadaExplodeTimer = 3; // encara no la feim funcionar

    Vector3 granadaDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Cam2d = GameObject.Find("2dcamera");

        granadaDirection = (new Vector3(player.transform.position.x, player.transform.position.y-2, player.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        granadaExplodeTimer -= Time.deltaTime;
        MoveGranada();
        if(granadaExplodeTimer <= 0)
        {
            explodeGranada();
        }
    }

    void explodeGranada()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) < 3)
        {
            player.GetComponent<playerController>().damage = true;
            player.GetComponent<playerController>().life -= 50;
        }
        Destroy(this.gameObject);
    }

    void MoveGranada()
    {
        //this.transform.position += granadaDirection * (Time.deltaTime * granadaSpeed);
        if(Vector2.Distance(this.transform.position, granadaDirection) > 1)
        this.transform.position += ((granadaDirection - transform.position).normalized * Time.deltaTime * granadaSpeed);
    }
}
