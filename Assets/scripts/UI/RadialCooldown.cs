using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialCooldown : MonoBehaviour
{
    public Image imageCooldown;
    public float cooldown;
    bool isCooldown;
    public GameObject player;

    void Start()
    {
        cooldown = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isCooldown = true;
        }

        if (isCooldown)
        {
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;

            if (imageCooldown.fillAmount >= 1)
            {
                imageCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
