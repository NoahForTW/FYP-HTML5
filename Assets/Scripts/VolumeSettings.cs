using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    public void SetMusicVolume()
    {
        float musicVolume = _musicSlider.value;
        _audioMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
    }

    public void SetSFXVolume()
    {
        float sfxVolume = _sfxSlider.value;
        _audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
    }
}
