using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GDTManager : MonoBehaviour
{
    [SerializeField] private GameObject customisePanel;
    [SerializeField] private GameObject titlePanel;


    private void Start()
    {
        // Ensure the canvas starts hidden
        customisePanel.SetActive(false);
        titlePanel.SetActive(true);
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
}
