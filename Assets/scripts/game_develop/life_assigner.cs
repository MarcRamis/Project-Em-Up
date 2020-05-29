using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class life_assigner : MonoBehaviour
{
    public GameObject player;
    public GameObject endLevel;
    public int lifes;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            Destroy(this.gameObject);
        }
        if(endLevel != null && endLevel.active == true)
        {
            lifes = player.GetComponent<playerController>().vidas;
        }
        else if(endLevel == null)
        {
            player = GameObject.Find("Player");
            player.GetComponent<playerController>().vidas = lifes;
            Destroy(this.gameObject);
        }
    }
}
