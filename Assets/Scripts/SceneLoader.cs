using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{   

    // Init the Scenes
    public enum Scene
    {
        GameScene,
        LoadingScene,
    }
    
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(Scene.LoadingScene.ToString());    

        SceneManager.LoadScene(scene.ToString());
    }
}
