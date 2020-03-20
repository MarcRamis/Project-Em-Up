using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal_2"), 0, Input.GetAxis("Vertical_2"));
        GetComponent<Rigidbody>().AddForce(input * speed);
    }
}