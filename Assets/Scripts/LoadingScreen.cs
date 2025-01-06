using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider progressBar; // Reference to a UI slider for progress.
    [SerializeField] private TMP_Text loadingText;

    [SerializeField] private TMP_Text loadingBarText;

    // Optional: Add hints to loading screen

    private void Start()
    {
        StartCoroutine(LoadTargetScene());
        StartCoroutine(AnimateLoadingText());
    }

    private IEnumerator LoadTargetScene()
    {
        float timer = 0;

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneLoader.TargetScene);
        operation.allowSceneActivation = false;

        // Update progress bar until the scene is ready.
        while (!operation.isDone)
        {
            // Progress ranges from 0.0f to 0.9f.
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            // Update the loading bar percentage text.
            if (loadingBarText != null)
            {
                loadingBarText.text = $"{(progress * 100):0}%"; // Display as a whole number percentage.
            }

            if (operation.progress >= 0.9f)
            {
                // Activate the scene once it's loaded.
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
            loadingText.text = "Loading " + new string('.', dotCount);

            // Increment dot count and wrap around after 3 dots.
            dotCount = (dotCount + 1) % 4;

            // Wait for 0.5 seconds before updating again.
            yield return new WaitForSeconds(0.5f);
        }
    }
}