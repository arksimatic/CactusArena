using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SfxVolumeChanger : MonoBehaviour
{
    private Slider slider;
    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void UpdateSfxVolume()
    {
        PlayerPrefs.SetFloat("sfxVolume", slider.value);
    }
}
