using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Init Sound Variables
public enum SoundType
{
    Drag
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    public SoundList[] soundList;

    private int[] currentSequenceIndex; // Track current index for each SoundType
    private Coroutine[] sequenceCoroutines; // Track coroutines for each SoundType
    private Coroutine[] loopCoroutines; // Track looping coroutines for each SoundType

    private void Awake()
    {
        instance = this;

        currentSequenceIndex = new int[soundList.Length]; // Initialize array size
        sequenceCoroutines = new Coroutine[soundList.Length]; // Initialize array size
        loopCoroutines = new Coroutine[soundList.Length]; // Initialize array size
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    /*
     * Can add different function to play sound, sequences, loop etc.
     * 
     */

    // Play Sound one shot / loop
    public static void PlaySoundOneShot(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        instance.audioSource.PlayOneShot(clips[0], volume);
    }
    
    // Play Sound Randomly
    public static void PlaySoundRandomly(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume);
    }

    // Play Sound in Sequences
    public static void PlaySoundInSequence(SoundType sound, float volume = 1f)
    {
        int soundIndex = (int)sound;
        if (instance.sequenceCoroutines[soundIndex] != null) return; // Prevent overlapping sequences
        instance.sequenceCoroutines[soundIndex] = instance.StartCoroutine(instance.PlaySoundSequenceCoroutine(soundIndex, volume));
    }

    // Stop the Sound
    public static void StopSoundSequence(SoundType sound)
    {
        int soundIndex = (int)sound;
        if (instance.sequenceCoroutines[soundIndex] != null)
        {
            instance.StopCoroutine(instance.sequenceCoroutines[soundIndex]);
            instance.sequenceCoroutines[soundIndex] = null;
        }
    }

    private IEnumerator PlaySoundSequenceCoroutine(int soundIndex, float volume)
    {
        AudioClip[] clips = soundList[soundIndex].Sounds;
        while (true)
        {
            if (currentSequenceIndex[soundIndex] >= clips.Length)
            {
                currentSequenceIndex[soundIndex] = 0; // Reset sequence index if it exceeds array length
            }

            audioSource.PlayOneShot(clips[currentSequenceIndex[soundIndex]], volume);
            yield return new WaitForSeconds(clips[currentSequenceIndex[soundIndex]].length); // Wait for the current clip to finish
            currentSequenceIndex[soundIndex]++;
        }
    }

    // Play Sound Loop
    public static void PlaySoundLoop(SoundType sound, float volume = 1f)
    {
        int soundIndex = (int)sound;
        if (instance.loopCoroutines[soundIndex] != null) return; // Prevent overlapping loops
        instance.loopCoroutines[soundIndex] = instance.StartCoroutine(instance.PlaySoundLoopCoroutine(soundIndex, volume));
    }

    // Stop Sound Loop
    public static void StopSoundLoop(SoundType sound)
    {
        int soundIndex = (int)sound;
        if (instance.loopCoroutines[soundIndex] != null)
        {
            instance.StopCoroutine(instance.loopCoroutines[soundIndex]);
            instance.loopCoroutines[soundIndex] = null;
        }
    }

    private IEnumerator PlaySoundLoopCoroutine(int soundIndex, float volume)
    {
        AudioClip clip = soundList[soundIndex].Sounds[0]; // Assume the first clip is to be looped
        while (true)
        {
            audioSource.PlayOneShot(clip, volume);
            yield return new WaitForSeconds(clip.length); // Wait for the clip to finish
        }
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds;}
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}