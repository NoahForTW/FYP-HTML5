using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GDTManager : MonoBehaviour
{
    [SerializeField] private GameObject switchLevel;
    [SerializeField] private GameObject customisePanel;
    [SerializeField] private GameObject titlePanel;


    private void Start()
    {
        // Ensure the canvas starts hidden
        switchLevel.SetActive(false);
        customisePanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    public void ToggleCanvas()
    {
        // Toggle the active state of the canvas
        switchLevel.SetActive(!switchLevel.activeSelf);
    }

    // For Customise Button
    public void ToggleCustomise()
    {
        customisePanel.SetActive(true);
        titlePanel.SetActive(false);
    }

    // To close Customise Screen
    public void CloseCustomise()
    {
        customisePanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    public void SwitchToScene(string sceneName)
    {
        // Check if the current scene is the target scene
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            Debug.Log("Already in the target scene: " + sceneName);
            return; // Do nothing if already in the scene
        }

        // Load the target scene
        SceneManager.LoadScene(sceneName);
    }
}
