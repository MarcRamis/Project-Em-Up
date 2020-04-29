using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screen_collision : MonoBehaviour
{
    public Camera Cam2d;
    public GameObject player;
    public GameObject playerCamera;
    public GameObject arrow;
    public int enemyCounter = 0;
    public float newPosX;
    public float speed = 1f;
    public bool enableArrow;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Condició per evaluar quan es té que moure la pantalla o parar-se
        if (enemyCounter <= 0 && Cam2d.WorldToScreenPoint(player.transform.position).x > 1300 / 2)
        {
            if(enableArrow == true)
            arrow.SetActive(true);
            newPosX = Mathf.Lerp(playerCamera.transform.position.x, player.transform.position.x, speed * Time.deltaTime);

            playerCamera.transform.position = new Vector3(newPosX, playerCamera.transform.position.y, playerCamera.transform.position.z);
        }
        else
        {
            arrow.SetActive(false);
        }
    }
}
