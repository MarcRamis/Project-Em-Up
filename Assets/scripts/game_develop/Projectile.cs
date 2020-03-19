using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal_2"), 0, Input.GetAxis("Vertical_2"));
        GetComponent<Rigidbody>().AddForce(input * speed);
    }
}
