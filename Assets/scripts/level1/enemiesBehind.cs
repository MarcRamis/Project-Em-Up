using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemiesBehind : MonoBehaviour
{
    public GameObject camera;
    public GameObject enemiesbehind;

    // Start is called before the first frame update

    private void Update()
    {
        if ((this.transform.position.x - camera.transform.position.x) <= 6)
        {
            enemiesbehind.SetActive(true);
        }
    }
}
