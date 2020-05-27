using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeChange : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = this.transform.GetComponent<Slider>();
    }

    void Update()
    {
        AudioListener.volume = slider.value;
    }
}
