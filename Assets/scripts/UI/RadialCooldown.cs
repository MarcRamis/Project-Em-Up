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
    public Image image;

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
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;

            image.color = Color.red;

            if (imageCooldown.fillAmount >= 1)
            {
                imageCooldown.fillAmount = 0.0f;

                // Mirem que el temporizador del cooldown sigui el mateix del player
                // D'aquesta forma ens evitem problemes a l'hora     de concordar els timings
                player.GetComponent<playerController>().lungeCooldown = 0.0f;
                isCooldown = false;
            }
        }
        else
            image.color = Color.green;
    }
}
