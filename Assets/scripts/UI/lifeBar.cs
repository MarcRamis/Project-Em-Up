using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeBar : MonoBehaviour
{
	public GameObject player;
    Scrollbar bar;
	
    // Start is called before the first frame update
    void Start()
    {   
        bar = this.GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.size = player.GetComponent<playerController>().life / 100.0f;
    }
}
