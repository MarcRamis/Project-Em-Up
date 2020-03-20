using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class area : MonoBehaviour
{
    public int enemyCounter = 0;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, camera.transform.position) <= 10)
        {
            camera.GetComponent<screen_collision>().enemyCounter = enemyCounter;
            Destroy(this.gameObject);
        }
    }
}
