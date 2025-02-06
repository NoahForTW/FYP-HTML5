using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScreen : MonoBehaviour
{
    [SerializeField] private Button websiteButton;

    [Tooltip("Add the URL here")]
    [SerializeField] private string websiteURL;

    void Start()
    {
        // Ensure the panel is hidden at the start
        gameObject.SetActive(false);

        // Add a listener to the button to open the URL
        websiteButton.onClick.AddListener(OpenWebsite);
    }

    // Call this function
    public void ShowPanel()
    {
        // Show the panel
        gameObject.SetActive(true);
    }

    void OpenWebsite()
    {
        // Open the URL in the default browser
        Application.OpenURL(websiteURL);
    }
}
