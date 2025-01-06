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

    // Optional: Add a hint dialogue into loading scene

    private void Start()
    {
        StartCoroutine(LoadTargetScene());
        StartCoroutine(AnimateLoadingText());
    }

    private IEnumerator LoadTargetScene()
    {
        float artificialProgress = 0; // Artificial progress value.
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneLoader.TargetScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Real progress ranges from 0.0f to 0.9f.
            float realProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Increment artificial progress gradually to simulate loading.
            if (artificialProgress < realProgress)
            {
                artificialProgress = Mathf.MoveTowards(artificialProgress, realProgress, Time.deltaTime * 0.5f); // Adjust speed as needed.
            }
            else if (realProgress >= 0.9f && artificialProgress < 1f)
            {
                artificialProgress = Mathf.MoveTowards(artificialProgress, 1f, Time.deltaTime * 0.3f); // Final slow increment to 100%.
            }

            // Update the progress bar and text.
            if (progressBar != null)
                progressBar.value = artificialProgress;

            if (loadingBarText != null)
                loadingBarText.text = $"{(artificialProgress * 100):0}%";

            // When artificial progress reaches 100%, allow the scene to activate.
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
            loadingText.text = "Loading " + new string('.', dotCount);

            // Increment dot count and wrap around after 3 dots.
            dotCount = (dotCount + 1) % 4;

            // Wait for 0.5 seconds before updating again.
            yield return new WaitForSeconds(0.25f);
        }
    }
}