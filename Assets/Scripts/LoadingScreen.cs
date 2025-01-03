using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar; // Reference to a UI slider for progress.

    private void Start()
    {
        StartCoroutine(LoadTargetScene());
    }

    private IEnumerator LoadTargetScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneLoader.TargetScene);
        operation.allowSceneActivation = false;

        // Update progress bar until the scene is ready.
        while (!operation.isDone)
        {
            // Progress ranges from 0.0f to 0.9f.
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
                progressBar.value = progress;

            if (operation.progress >= 0.9f)
            {
                // Activate the scene once it's loaded.
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
