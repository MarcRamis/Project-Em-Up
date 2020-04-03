using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_movement_level1 : MonoBehaviour
{
    public GameObject cameraPlayer;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(cameraPlayer.transform.position.x / 100, this.transform.position.y, this.transform.position.z);
    }
}
