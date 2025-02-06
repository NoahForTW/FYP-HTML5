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
    Wrong,
    Hurt,
    Explosion,
    Squish,
    Hover,
    Click
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxAudioSource; // For sound effects
    [SerializeField] private AudioSource musicAudioSource; // For background music

    [Header("Sound Effects")]
    public SoundList[] soundList;

    [Header("Music")]
    public SceneMusic[] sceneMusic; // Array of scene-specific music

    [Header("Settings")]
    [Range(0, 1)] public float sfxVolume = 1f; // SFX volume (default: 100%)
    [Range(0, 1)] public float musicVolume = 1f; // Music volume (default: 100%)

    private float fadeDuration = 1f; // Duration of fade in/out for music

    [Tooltip("To enable or disable the audio in the game")]
    public bool canAudio = false;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Ensure AudioSources are set up
        if (sfxAudioSource == null)
        {
            sfxAudioSource = gameObject.AddComponent<AudioSource>();
            sfxAudioSource.loop = false; // SFX should not loop
        }

        if (musicAudioSource == null)
        {
            musicAudioSource = gameObject.AddComponent<AudioSource>();
            musicAudioSource.loop = true; // Music should loop
        }

        // Load player settings for SFX and music volume
        //LoadAudioSettings();

        // Subscribe to scene change events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // Apply initial volumes
        UpdateVolumes();
    }

    // Update SFX and music volumes
    public void UpdateVolumes()
    {
        sfxAudioSource.volume = sfxVolume;
        musicAudioSource.volume = musicVolume;
    }

    /*
     * Can add different function to play sound, sequences, loop etc.
     * Just called the function with AudioManager.TheFunction(EnumType, Volume) Volume can leave blank if you do not want to adjust it
     */

    // Play Sound one shot
    // Play a sound effect
    public void PlaySoundOneShot(SoundType sound, float volumeMultiplier = 1f)
    {
        if(!canAudio) return;

        AudioClip[] clips = soundList[(int)sound].Sounds;
        if (clips.Length > 0)
        {
            sfxAudioSource.PlayOneShot(clips[0], sfxVolume * volumeMultiplier);
        }
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

        SceneMusic musicForScene = null;
        foreach (var sceneMusicEntry in sceneMusic)
        {
            if (scene.name == sceneMusicEntry.sceneName)
            {
                musicForScene = sceneMusicEntry;
                break;
            }
        }

        if (musicForScene != null)
        {
            if (musicAudioSource.isPlaying)
            {
                StartCoroutine(CrossfadeMusic(musicForScene.musicClip, musicForScene.volume));
            }
            else
            {
                PlayMusic(musicForScene.musicClip, musicForScene.volume);
            }
        }
        else
        {
            StopMusic();
        }
    }

    // Play music directly
    private void PlayMusic(AudioClip clip, float volume)
    {

        musicAudioSource.clip = clip;
        musicAudioSource.volume = musicVolume * volume;
        musicAudioSource.Play();
    }

    // Stop music directly
    private void StopMusic()
    {
        musicAudioSource.Stop();
    }

    // Crossfade between current music and new music
    private IEnumerator CrossfadeMusic(AudioClip newClip, float newVolume)
    {
        // Fade out current music
        float startVolume = musicAudioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicAudioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }
        musicAudioSource.Stop();

        // Play new music and fade in
        musicAudioSource.clip = newClip;
        musicAudioSource.Play();
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicAudioSource.volume = Mathf.Lerp(0, musicVolume * newVolume, t / fadeDuration);
            yield return null;
        }
    }

    // Fade out music
    public IEnumerator FadeOutMusic()
    {
        float startVolume = musicAudioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicAudioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }
        musicAudioSource.Stop();
    }

    // Fade in music
    public IEnumerator FadeInMusic(AudioClip clip, float volume)
    {
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicAudioSource.volume = Mathf.Lerp(0, musicVolume * volume, t / fadeDuration);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from scene change events
        SceneManager.sceneLoaded -= OnSceneLoaded;
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

[Serializable]
public class SceneMusic
{
    public string sceneName; // Name of the scene
    public AudioClip musicClip; // Music clip to play for this scene
    public float volume = 1f; // Volume for this music clip
}