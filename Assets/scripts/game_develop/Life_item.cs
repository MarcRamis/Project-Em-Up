using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_item : MonoBehaviour
{
    public GameObject player;
    public GameObject lifeGainEffectSound;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) < 1)
        {
            if(player.GetComponent<playerController>().life + 40 <= 100)
            {
                player.GetComponent<playerController>().life += 40;
                lifeGainEffectSound.SetActive(true);
                Destroy(this.gameObject);
            }
            else
            {
                player.GetComponent<playerController>().life = 100;
                lifeGainEffectSound.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }
}
