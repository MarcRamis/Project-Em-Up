using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPowerUp : MonoBehaviour
{
    Scrollbar bar;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        bar = this.GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.size = player.GetComponent<playerController>().ultimateAttackPlus / 100.0f;
    }
}
