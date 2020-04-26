using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    public GameObject Cam2d;
    public GameObject player;

    //Granada components
    public float granadaSpeed = 10;
    public float granadaExplodeTimer; // encara no la feim funcionar

    Vector3 granadaDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Cam2d = GameObject.Find("2dcamera");

        granadaDirection = player.transform.position - this.transform.position;
        granadaDirection /= granadaDirection.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        MoveGranada();
        /*
        if (Cam2d.GetComponent<Camera>().WorldToScreenPoint(this.transform.position).x < -100 
            || Cam2d.GetComponent<Camera>().WorldToScreenPoint(this.transform.position).x > 1500)
        {
            DestroyGranada();
        }
        */
    }

    void DestroyGranada()
    {
        Destroy(this.gameObject);
    }

    void MoveGranada()
    {
        this.transform.position += granadaDirection * (Time.deltaTime * granadaSpeed);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            DestroyGranada();
        }
    }
}
