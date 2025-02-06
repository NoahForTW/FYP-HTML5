using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;
using System;
using System.Linq;

public class Cutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public TextMeshProUGUI cutsceneText; // Reference to the TextMeshPro UI component

    public List<string> cutsceneURLs = new List<string>(); // List of video URLs
    public List<CutSceneTexts> cutsceneTexts = new List<CutSceneTexts>(); // List of text captions

    private int currentCutsceneIndex = 0; // Keeps track of the current cutscene
    private bool canPressSpace = true; // Cooldown flag

    SceneLoader sceneLoader;
    int currentTextIndex = 0;

    bool IsTextDone = false;

    Coroutine cutsceneTextPrinting;
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
            if (!IsTextDone)
            {
                UpdateCutSceneText();
                return;
            }
                
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
        IsTextDone = false;
        UpdateCutSceneText();
        
    }

    void UpdateCutSceneText()
    {
        if (cutsceneText != null)
        {
            List<CutSceneTexts> texts = cutsceneTexts.Where(text => text.cutsceneIndex == currentCutsceneIndex).ToList(); 
            if (cutsceneTextPrinting != null)
                StopCoroutine(cutsceneTextPrinting);
            cutsceneTextPrinting = StartCoroutine(CutsceneText(texts[currentTextIndex].text));
            currentTextIndex++;
            if (currentTextIndex >= texts.Count)
            {
                currentTextIndex = 0;
                IsTextDone = true;
            }
        }
    }

    IEnumerator CutsceneText(string text)
    {
        // set the text to the full line, but set the visible characters to 0
        cutsceneText.text = text;
        cutsceneText.maxVisibleCharacters = 0;

        // display each letter one at a time
        foreach (char c in text.ToCharArray())
        {
            cutsceneText.maxVisibleCharacters++;
            yield return new WaitForSecondsRealtime(0.05f);
            
        }

    }
}

[Serializable]
public class CutSceneTexts
{
    public string text;
    public int cutsceneIndex;
}
