using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeChange : MonoBehaviour
{
    public Slider slider;
    public GameObject volumeObject;

    private void Start()
    {
        volumeObject = GameObject.Find("volume");
        slider = this.transform.GetComponent<Slider>();
        slider.value = AudioListener.volume;
    }

    void Update()
    {
        volumeObject.GetComponent<volume>().volumeitem = slider.value;
    }
}
