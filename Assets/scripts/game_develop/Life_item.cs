using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_item : MonoBehaviour
{
    public int maxHp = 100;
    public GameObject player;
    public GameObject lifeGainEffectSound;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) < 1 && player.GetComponent<playerController>().life != maxHp)
        {
            if(player.GetComponent<playerController>().life + 40 <= maxHp)
            {
                player.GetComponent<playerController>().life += 40;
                lifeGainEffectSound.SetActive(true);
                Destroy(this.gameObject);
            }
            else
            {
                player.GetComponent<playerController>().life = maxHp;
                lifeGainEffectSound.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }
}
