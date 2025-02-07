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

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Start()
    {
        // Rachel stuff 
        // Initialize sliders with saved values or defaults
        AudioSettings data = SavePlayerData.Instance.LoadData<PlayerData>().audioSettings;
        _musicSlider.value = data.musicVolume; // Default to 75% volume
        _sfxSlider.value = data.sfxVolume; // Default to 75% volume

        // Set initial volumes
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        float musicVolume = _musicSlider.value;
        _audioMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
        // Save the volume setting
        AudioSettings data = SavePlayerData.Instance.LoadData<PlayerData>().audioSettings;
        data.musicVolume = musicVolume;
        SavePlayerData.Instance.SaveData(data);
    }

    public void SetSFXVolume()
    {
        float sfxVolume = _sfxSlider.value;
        _audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
        // Save the volume setting
                AudioSettings data = SavePlayerData.Instance.LoadData<PlayerData>().audioSettings;
        data.musicVolume = sfxVolume;
        SavePlayerData.Instance.SaveData(data);
    }
}
