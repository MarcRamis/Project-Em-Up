using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeBar : MonoBehaviour
{
	public GameObject BarLife;
	public GameObject player;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BarLife.GetComponent<RectTransform>().transform.localScale = new Vector3(player.GetComponent<playerController>().life/3, 0.2394248f, 0.025999f);
    }
}
