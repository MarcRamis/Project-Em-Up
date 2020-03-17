using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWATS : MonoBehaviour
{
	public GameObject player;
    public Camera Cam2d;
    public float speed;

    // Update is called once per frame
    /*
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player) < 3)
        {
            this.transform.position -= transform.position.normalized * Time.deltaTime * speed;
        }
    }
    */
}
