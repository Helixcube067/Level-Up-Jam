using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class SettingsMenu : MonoBehaviour
{
    private SoundbankScript _Soundbank;
    Slider _VolumeSlider;
    void Start()
    {
        _Soundbank = GameObject.Find("SoundBank").GetComponent<SoundbankScript>();
        _VolumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        _VolumeSlider.value = _Soundbank.GetVolume();
    }

    public void SetVolume()
    {
        _Soundbank.SetVolume(_VolumeSlider.value);
    }
}
