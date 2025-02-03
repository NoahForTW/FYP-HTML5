using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine.SceneManagement;

// Init Sound Variables
/*
 * Declare the type of sound you want to play under this enum
*/
public enum SoundType
{
    Jumping,
    PickUpCoin,
    Walking,
    Successful,
    PickUpPotion,
    Lever,
    Correct,
    Wrong
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    public SoundList[] soundList;

    [Tooltip("To enable or disable the audio in the game")]
    public bool canAudio;

    private void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make sure the AudioManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances
            return;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /*
     * Can add different function to play sound, sequences, loop etc.
     * Just called the function with AudioManager.TheFunction(EnumType, Volume) Volume can leave blank if you do not want to adjust it
     */

    // Play Sound one shot
    public void PlaySoundOneShot(SoundType sound, float volume = 1)
    {
        if(!canAudio) return;

        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        instance.audioSource.PlayOneShot(clips[0], volume);
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
#endif
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool hasSettings = SavePlayerData.Instance.LoadData<GameData>().SettingsActive.Any(settings => settings.settingsName == GetType().Name);
        if (hasSettings)
            canAudio = true;
    }

    public override string ToString()
    {
        return GetType().Name;
    }
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds;}
    [ReadOnly] public string name;
    [SerializeField] private AudioClip[] sounds;
}