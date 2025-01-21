using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;

    [ReadOnly]
    public bool isPaused = false; // Track pause state

    void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(false);
        //Time.timeScale = 1; // Ensure time is running normally at the start

        // Subscribe to the Pause action
        PlayerController.Instance.playerAction.AddListener(OnPlayerAction);
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        PlayerController.Instance.playerAction.RemoveListener(OnPlayerAction);
    }

    private void OnPlayerAction(PlayerAction action)
    {
        if (action == PlayerAction.Pause)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false); // Ensure settings panel is hidden
        pauseMenu.SetActive(true);
        //Time.timeScale = 0; // Freeze time
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false); // Ensure settings panel is hidden
        pauseMenu.SetActive(false);
        //Time.timeScale = 1; // Resume time
        isPaused = false;
    }

    public void OpenSettings()
    {
        pausePanel.SetActive(false); // Hide Pause Menu
        settingsPanel.SetActive(true); // Show Settings Panel
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Hide Settings Panel
        pausePanel.SetActive(true); // Show Pause Menu
    }
}
