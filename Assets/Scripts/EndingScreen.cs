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
        // Add a listener to the button to open the URL
        websiteButton.onClick.AddListener(OpenWebsite);
    }

    void OpenWebsite()
    {
        // Open the URL in the default browser
        Application.OpenURL(websiteURL);
    }
}
