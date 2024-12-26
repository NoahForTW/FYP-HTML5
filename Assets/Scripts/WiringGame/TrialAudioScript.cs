using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class TrialAudioScript : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    public void PlaySFX(AudioClip clip, float volume = 1, bool randomPitching = false, bool loop = false)
//    {
//        if (clip == null)
//        {
//            Debug.LogWarning("Audio : Provided AudioClip is null!");
//            return;
//        }

//        AudioSource source = GetAvailableAudioSource(_sfxSources, _sfxParent);
//        if (source != null)
//        {
//            source.clip = clip;
//            source.volume = volume;
//            source.loop = loop;

//            if (randomPitching)
//            {
//                // Slightly randomize pitch within a narrow range
//                source.pitch = Random.Range(0.9f, 1.1f);
//                Debug.Log($"Audio : SFX Pitched to {source.pitch}");
//            }
//            else
//            {
//                // Reset to default pitch
//                source.pitch = 1f;
//            }

//            source.Play();
//        }
//    }

//    private AudioSource GetAvailableAudioSource(List<AudioSource> sources, GameObject parent)
//    {
//        // Try to find an available (not playing) AudioSource
//        AudioSource availableSource = sources.FirstOrDefault(source => !source.isPlaying);

//        if (availableSource != null)
//        {
//            return availableSource;
//        }

//        // If no available source, create a new one and add it to the list
//        AudioSource newSource = parent.AddComponent<AudioSource>();

//        // Configure the new AudioSource based on its parent type (SFX, BGM, or VL)
//        if (parent == _sfxParent)
//        {
//            newSource.outputAudioMixerGroup = GlobalMixer.FindMatchingGroups("SFX")[0];
//        }
//        else if (parent == _bgmParent)
//        {
//            newSource.outputAudioMixerGroup = GlobalMixer.FindMatchingGroups("BGM")[0];
//        }
//        else if (parent == _vlParent)
//        {
//            newSource.outputAudioMixerGroup = GlobalMixer.FindMatchingGroups("VL")[0];
//        }

//        sources.Add(newSource);
//        return newSource;
//    }
//}
