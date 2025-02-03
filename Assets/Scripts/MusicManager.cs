using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName; // Name of the scene
        public AudioClip musicClip; // Music clip to play for this scene
        public float volume = 1f; // Volume for this music clip
    }

    public SceneMusic[] sceneMusic; // Array of scene-specific music

    private AudioSource audioSource;
    private float fadeDuration = 1f; // Duration of fade in/out

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true; // Ensure music loops
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene change events
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the music for the current scene
        SceneMusic musicForScene = null;
        foreach (var sceneMusicEntry in sceneMusic)
        {
            if (scene.name == sceneMusicEntry.sceneName)
            {
                musicForScene = sceneMusicEntry;
                break;
            }
        }

        // Play the music for the current scene
        if (musicForScene != null)
        {
            if (audioSource.isPlaying)
            {
                // Fade out current music and fade in new music
                StartCoroutine(CrossfadeMusic(musicForScene.musicClip, musicForScene.volume));
            }
            else
            {
                // Play new music directly
                PlayMusic(musicForScene.musicClip, musicForScene.volume);
            }
        }
        else
        {
            // Stop music if no music is defined for the scene
            StopMusic();
        }
    }

    // Play music directly
    private void PlayMusic(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    // Stop music directly
    private void StopMusic()
    {
        audioSource.Stop();
    }

    // Crossfade between current music and new music
    private IEnumerator CrossfadeMusic(AudioClip newClip, float newVolume)
    {
        // Fade out current music
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }
        audioSource.Stop();

        // Play new music and fade in
        audioSource.clip = newClip;
        audioSource.Play();
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, newVolume, t / fadeDuration);
            yield return null;
        }
    }

    // Fade out music
    public IEnumerator FadeOutMusic()
    {
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }
        audioSource.Stop();
    }

    // Fade in music
    public IEnumerator FadeInMusic(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.Play();
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, volume, t / fadeDuration);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from scene change events
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}