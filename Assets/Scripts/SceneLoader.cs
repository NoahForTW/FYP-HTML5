using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{   

    // Init the Scenes
    public enum Scene
    {
        None,
        GDTScene,
        LoadingScene,
    }
    
    public static void Load(Scene scene)
    {
        SceneManager.LoadSceneAsync(Scene.LoadingScene.ToString());
    }
}
