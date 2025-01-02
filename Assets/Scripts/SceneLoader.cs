using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{  
    public static string TargetScene; // Store the scene name to load.

    // Method to be called by buttons
    public void LoadSceneWithLoading(string sceneName)
    {
        TargetScene = sceneName; // Store the target scene name.
        SceneManager.LoadScene("LoadingScene"); // Load the loading scene.
    }
}
