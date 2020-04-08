using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricada_collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void onTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(Input.GetKey(KeyCode.D))
            {
                other.transform.position = new Vector3(other.transform.position.x - 1, other.transform.position.y, other.transform.position.z);
            }

            else if (Input.GetKey(KeyCode.A) )
            {
                other.transform.position = new Vector3(other.transform.position.x + 1, other.transform.position.y, other.transform.position.z);
            }
        }
    }
}