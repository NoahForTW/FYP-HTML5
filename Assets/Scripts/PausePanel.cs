using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    [ReadOnly]
    public bool isPaused = false; // Track pause state

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1; // Ensure time is running normally at the start
    }

    void Update()
    {
        // Toggle pause state when Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Time.timeScale = 0; // Freeze time
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1; // Resume time
        isPaused = false;
    }
}
