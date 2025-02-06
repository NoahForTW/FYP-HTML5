using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEnded : MonoBehaviour
{
    public SceneLoader sceneLoader;

    // This method is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            sceneLoader.LoadSceneWithLoading("EndingScene");
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
