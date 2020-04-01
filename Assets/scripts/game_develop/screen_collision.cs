using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screen_collision : MonoBehaviour
{
    public Camera Cam2d;
    public GameObject player;
    public GameObject playerCamera;
    public int enemyCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Condició per evaluar quant es té que moure la pantalla o parar-se
        if (enemyCounter <= 0 && Cam2d.WorldToScreenPoint(player.transform.position).x > 1300/3)
        {
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x + 0.02f, playerCamera.transform.position.y, playerCamera.transform.position.z);
        }
    }
}
