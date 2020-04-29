using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialCooldown : MonoBehaviour
{
    public Image imageCooldown;
    public float cooldown;
    public bool isCooldown;
    public GameObject player;
    public GameObject imageNuñez;

    void Start()
    {
        cooldown = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<playerController>().lunge == true)
        {
            isCooldown = true;
        }

        if (isCooldown)
        {
            imageNuñez.SetActive(false);
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;


            if (imageCooldown.fillAmount >= 1)
            {
                imageCooldown.fillAmount = 0.0f;

                // Miram que el temporizador del cooldown sigui el mateix del player
                // D'aquesta forma ens evitam problemes a la de concordar els timings
                player.GetComponent<playerController>().lungeCooldown = 0.0f;
                isCooldown = false;
            }
        }
        else
        {
            imageNuñez.SetActive(true);
        }
    }
}
