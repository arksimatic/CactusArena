using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeChanger : MonoBehaviour
{
    private AudioSource audioSource;
    private Slider slider;
    void Start()
    {
        audioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        slider = this.gameObject.GetComponent<Slider>();
        slider.value = audioSource.volume;
    }

    public void UpdateMusicVolume()
    {
        audioSource.volume = slider.value;
    }
}
