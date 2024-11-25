using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void StartButton()
    {
        SceneLoader.Load(SceneLoader.Scene.GameScene);

        Debug.Log("Start Game");
    }
}
