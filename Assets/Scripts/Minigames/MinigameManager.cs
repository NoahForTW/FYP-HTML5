using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinigameType
{
    None,
    Audio,
    Variable_Gear,
    Variable_Spelling,
    Variable_Wires,
    Drawing,
    ModelTexture,
    Frames,
}

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    public Minigame currentMinigame;
    MinigameType currentMinigameType;

    GameObject parentCanvas;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        parentCanvas = GameObject.FindGameObjectWithTag("ParentCanvas");
    }
    public void SetMinigame(MinigameType type)
    {
        currentMinigameType = type;
        if (type == MinigameType.None)
        {
            currentMinigame = null;
            return;
        }

        Minigame[] games = parentCanvas.GetComponentsInChildren<Minigame>(true);
        foreach(var game in games)
        {
            if (game.minigameType == type)
            {
                currentMinigame = game;
                break;
            }
        }
    }

    public MinigameType GetCurrentMinigameType()
    {
        return currentMinigameType;
    }

    public Minigame GetCurrentMinigame()
    {
        return currentMinigame;
    }

    bool IsCurrentActive()
    {
        if (currentMinigame == null)
            return false;

        return currentMinigame.gameObject.activeSelf;
    }

    private void Update()
    {
        if (currentMinigame != null)
        {
            if (currentMinigameType != MinigameType.None
             && !IsCurrentActive())
            {
                currentMinigame.gameObject.SetActive(true);
            }

            if (currentMinigame.isCompleted)
            {
                currentMinigame.gameObject.SetActive(false);
                SetMinigame(MinigameType.None);
            }
        }

    }
}
