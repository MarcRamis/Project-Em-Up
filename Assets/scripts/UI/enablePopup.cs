using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enablePopup : MonoBehaviour
{
    public GameObject camera;
    public GameObject popup;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((this.transform.position.x - camera.transform.position.x) <= 15)
        {
            popup.SetActive(true);
            Destroy(this);
        }
    }
}
