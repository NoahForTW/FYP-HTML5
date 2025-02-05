using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDTManager : MonoBehaviour
{
    [SerializeField] private GameObject customisePanel;
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private Animator creditsAnimator;

    private void Start()
    {
        // Ensure the canvas starts hidden
        customisePanel.SetActive(false);
        creditsPanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    // For Customise Button
    public void ToggleCustomise()
    {
        customisePanel.SetActive(true);
        titlePanel.SetActive(false);
    }

    public void ToggleCredits()
    {
        creditsPanel.SetActive(true);
        titlePanel.SetActive(false);

        // Start the coroutine to wait for the animation to finish
        StartCoroutine(WaitForCreditsAnimation());
    }

    // To close Customise Screen
    public void CloseCustomise()
    {
        customisePanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    private IEnumerator WaitForCreditsAnimation()
    {
        // Wait for the animation to finish
        yield return new WaitForSeconds(creditsAnimator.GetCurrentAnimatorStateInfo(0).length);

        // After the animation is done, hide the credits panel and show the title panel
        creditsPanel.SetActive(false);
        titlePanel.SetActive(true);
    }
}
