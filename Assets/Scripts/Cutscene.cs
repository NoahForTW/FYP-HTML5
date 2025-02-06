using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public TextMeshProUGUI cutsceneText; // Reference to the TextMeshPro UI component

    public List<string> cutsceneURLs = new List<string>(); // List of video URLs
    public List<string> cutsceneTexts = new List<string>(); // List of text captions

    private int currentCutsceneIndex = 0; // Keeps track of the current cutscene
    private bool canPressSpace = true; // Cooldown flag

    public SceneLoader sceneLoader;

    void Start()
    {
        if (cutsceneURLs.Count > 0)
        {
            PlayCutscene(0); // Play the first cutscene at start
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canPressSpace)
        {
            StartCoroutine(NextCutscene());
        }
    }

    IEnumerator NextCutscene()
    {
        canPressSpace = false; // Disable input for cooldown

        currentCutsceneIndex++;
        if (currentCutsceneIndex < cutsceneURLs.Count)
        {
            PlayCutscene(currentCutsceneIndex);
        }
        else 
        {
            sceneLoader.LoadSceneWithLoading("GDTLevel");
            SceneManager.LoadScene("LoadingScene");
        }

        yield return new WaitForSeconds(1f); // 1-second cooldown
        canPressSpace = true; // Re-enable input
    }

    void PlayCutscene(int index)
    {
        if (videoPlayer != null)
        {
            videoPlayer.url = cutsceneURLs[index];
            videoPlayer.Play();
        }

        if (cutsceneText != null && index < cutsceneTexts.Count)
        {
            cutsceneText.text = cutsceneTexts[index]; // Change the text for each cutscene
        }
    }
}