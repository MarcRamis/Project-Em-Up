using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableEndLevel : MonoBehaviour
{
    public GameObject camera;
    public GameObject endLevel;

    // Update is called once per frame
    void Update()
    {
        if((this.transform.position.x - camera.transform.position.x) <= 1)
        {
            endLevel.SetActive(true);
        }
    }
}
