using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_bullet : MonoBehaviour
{
    float destroyTimer;

    void Start()
    {
        destroyTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        destroyTimer += Time.deltaTime;
        
        if (destroyTimer >= 0.2f)
        {
            Destroy(this.gameObject);
            destroyTimer = 0.0f;
        }
    }
}
