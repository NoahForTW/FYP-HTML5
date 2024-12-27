using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class TrialAudioScript : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField]
    private AudioSource _sfxSource;

    [SerializeField]
    private AudioSource _bgmSource, _prioritySFXSource;

    [SerializeField]
    public AudioMixer GlobalMixer;

    [SerializeField]
    private GameObject _bgmParent, _sfxParent, _vlParent;

    [SerializeField] private List<AudioSource> _bgmSources = new();
    [SerializeField] private List<AudioSource> _sfxSources = new();
    [SerializeField] private List<AudioSource> _vlSources = new();

    [SerializeField] private List<AudioClip> _bgmClips = new();
    [SerializeField] private List<AudioClip> _sfxClips = new();
    [SerializeField] private List<AudioClip> _vlClips = new();

    //private float _fadeDuration = 5f;
    //private bool _lerpingVolume;
    //private float _targetEndVolume;
    //private float _lerpSpeed = 1;
    //private float _currentBGMVolume = 1;

    void Awake()
    {
        _bgmSources = _bgmParent.GetComponents<AudioSource>().ToList();
        _sfxSources = _sfxParent.GetComponents<AudioSource>().ToList();
        _vlSources = _vlParent.GetComponents<AudioSource>().ToList();

        _bgmClips = new List<AudioClip>(Resources.LoadAll<AudioClip>("Audio/BGM"));
        _sfxClips = new List<AudioClip>(Resources.LoadAll<AudioClip>("Audio/SFX"));
        _vlClips = new List<AudioClip>(Resources.LoadAll<AudioClip>("Audio/VL"));
    }

    public void PlaySFX(AudioClip clip, float volume = 1, bool randomPitching = false, bool loop = false)
    {
        if (clip == null)
        {
            Debug.LogWarning("Audio : Provided AudioClip is null!");
            return;
        }

        AudioSource source = GetAvailableAudioSource(_sfxSources, _sfxParent);
        if (source != null)
        {
            source.clip = clip;
            source.volume = volume;
            source.loop = loop;

            if (randomPitching)
            {
                // Slightly randomize pitch within a narrow range
                source.pitch = Random.Range(0.9f, 1.1f);
                Debug.Log($"Audio : SFX Pitched to {source.pitch}");
            }
            else
            {
                // Reset to default pitch
                source.pitch = 1f;
            }

            source.Play();
        }
    }

    private AudioSource GetAvailableAudioSource(List<AudioSource> sources, GameObject parent)
    {
        // Try to find an available (not playing) AudioSource
        AudioSource availableSource = sources.FirstOrDefault(source => !source.isPlaying);

        if (availableSource != null)
        {
            return availableSource;
        }

        // If no available source, create a new one and add it to the list
        AudioSource newSource = parent.AddComponent<AudioSource>();

        // Configure the new AudioSource based on its parent type (SFX, BGM, or VL)
        if (parent == _sfxParent)
        {
            newSource.outputAudioMixerGroup = GlobalMixer.FindMatchingGroups("SFX")[0];
        }
        else if (parent == _bgmParent)
        {
            newSource.outputAudioMixerGroup = GlobalMixer.FindMatchingGroups("BGM")[0];
        }
        else if (parent == _vlParent)
        {
            newSource.outputAudioMixerGroup = GlobalMixer.FindMatchingGroups("VL")[0];
        }

        sources.Add(newSource);
        return newSource;
    }
}
