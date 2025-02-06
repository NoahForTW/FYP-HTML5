using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider progressBar; // Reference to a UI slider for progress.
    [SerializeField] private TMP_Text loadingText; // Reference to the "Loading..." text.
    [SerializeField] private TMP_Text loadingBarText; // Reference to the percentage text.
    [SerializeField] private TMP_Text tipText; // Reference to the text displaying tips.
    [SerializeField] private Image panelImage; 
    [SerializeField] private Sprite[] gdtBG;
    [SerializeField] private Sprite[] agveFG;
    [SerializeField] private Sprite[] titleBG;
    [SerializeField] private TipsDataSO tipsDataSO;
    
    private List<string> tips = new List<string>(); // List to store tips from JSON.

    private void Start()
    {
        Time.timeScale = 1;

        SetBackgroundImage();
        DisplayRandomTip();
        StartCoroutine(LoadTargetScene());
        StartCoroutine(AnimateLoadingText());
    }

    private void SetBackgroundImage()
    {
        if (panelImage == null)
        {
            Debug.LogError("Background panel image is not assigned.");
            return;
        }

        Sprite[] selectedArray = null;

        // Determine which background array to use based on the target scene
        if (SceneLoader.TargetScene == "GDTLevel") // Replace with your actual scene names
        {
            selectedArray = gdtBG;
        }
        else if (SceneLoader.TargetScene == "AGVEScene")
        {
            selectedArray = agveFG;
        }

        else if (SceneLoader.TargetScene == "TitleScene")
        {
            selectedArray = titleBG;
        }
        else if (SceneLoader.TargetScene == "CutsceneScene")
        {
            selectedArray = titleBG;
        }

        if (selectedArray != null && selectedArray.Length > 0)
        {
            // Randomly pick a background image from the selected array
            Sprite randomBackground = selectedArray[Random.Range(0, selectedArray.Length)];
            panelImage.sprite = randomBackground;
        }
        else
        {
            Debug.LogError("No backgrounds found for the selected scene.");
        }
    }

    // TODO: Add a Scene Transition

    private void DisplayRandomTip()
    {
        if (tipsDataSO != null && tipsDataSO.tips.Count > 0)
        {
            string randomTip = tipsDataSO.tips[Random.Range(0, tipsDataSO.tips.Count)];
            if (tipText != null)
            {
                tipText.text = randomTip;
            }
        }
        else
        {
            Debug.LogError("TipsDataSO is not assigned or contains no tips.");
        }
    }

    private IEnumerator LoadTargetScene()
    {
        float artificialProgress = 0; // Artificial progress value.
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneLoader.TargetScene);
        operation.allowSceneActivation = false;

        // Variables for random loading speed
        float currentSpeed = Random.Range(0.2f, 0.5f); // Initial random speed.
        float speedChangeInterval = Random.Range(0.5f, 1f); // Time before changing speed.

        float elapsedTime = 0f;

        // Threshold for changing the tip
        float tipChangeThreshold = Random.Range(0.2f, 0.8f);
        bool tipChanged = false;

        while (!operation.isDone)
        {
            // Real progress ranges from 0.0f to 0.9f.
            float realProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Increment artificial progress with dynamic speed.
            artificialProgress = Mathf.MoveTowards(artificialProgress, realProgress, Time.deltaTime * currentSpeed);

            // Change speed periodically
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= speedChangeInterval)
            {
                currentSpeed = Random.Range(0.2f, 0.7f); // Pick a new speed.
                speedChangeInterval = Random.Range(1f, 2.5f); // Set a new interval.
                elapsedTime = 0f; // Reset the timer.
            }

            // Ensure final progress towards 100% is slower and smoother.
            if (realProgress >= 0.9f && artificialProgress < 1f)
            {
                artificialProgress = Mathf.MoveTowards(artificialProgress, 1f, Time.deltaTime * 0.2f);
            }

            // Update the progress bar and text.
            if (progressBar != null)
                progressBar.value = artificialProgress;

            if (loadingBarText != null)
                loadingBarText.text = $"{(artificialProgress * 100):0}%";

            // Check for the threshold and change the tip
            if (!tipChanged && artificialProgress >= tipChangeThreshold)
            {
                DisplayRandomTip(); // Change the tip
                tipChanged = true;  // Ensure this happens only once
            }

            // Allow scene activation once progress is 100%.
            if (artificialProgress >= 1f && realProgress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private IEnumerator AnimateLoadingText()
    {
        int dotCount = 0;

        while (true)
        {
            // Update text based on the number of dots.
            loadingText.text = "Loading" + new string('.', dotCount);

            // Increment dot count and wrap around after 3 dots.
            dotCount = (dotCount + 1) % 4;

            // Wait for 0.5 seconds before updating again.
            yield return new WaitForSeconds(0.25f);
        }
    }

    [System.Serializable]
    private class TipsData
    {
        public List<string> tips;
    }
}