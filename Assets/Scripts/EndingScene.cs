using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : MonoBehaviour
{
    [SerializeField] private GameObject panel; // Assign this in the Inspector
    [SerializeField] private float delayTime = 3f; // Adjust the delay time as needed

    void Start()
    {
        panel.SetActive(false); // Ensure the panel is hidden initially
        StartCoroutine(ShowPanelAfterDelay());
    }

    IEnumerator ShowPanelAfterDelay()
    {
        yield return new WaitForSeconds(delayTime); // Wait for the specified time
        panel.SetActive(true); // Show the panel
    }
}
