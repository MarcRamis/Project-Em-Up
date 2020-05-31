using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volume : MonoBehaviour
{
    public float volumeitem = 1;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("volume") != null && GameObject.Find("volume") != this.gameObject)
        Destroy(GameObject.Find("volume"));

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = volumeitem;
    }
}
