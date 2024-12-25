using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    TimerCanvas timerCanvas;
    TMP_Text timerUI;
    GameObject notification;

    float gameTimer;
    bool pauseTimer = false;
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
        timerCanvas = parentCanvas.GetComponentInChildren<TimerCanvas>(true);
        timerUI = timerCanvas?.timerText;
        notification = timerCanvas?.notification;

        if (timerCanvas != null)
        {
            timerCanvas.gameObject.SetActive(false);
        }
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

    public string GetGameTimerInFormat()
    {
        int min = Mathf.FloorToInt(gameTimer / 60);
        int seconds = Mathf.FloorToInt(gameTimer % 60);
        return string.Format("{0:00}:{1:00}", min, seconds);
    }

    bool IsCurrentActive()
    {
        if (currentMinigame == null)
            return false;

        return currentMinigame.gameObject.activeSelf;
    }

    void StartMinigame()
    {
        pauseTimer = false;
        timerCanvas?.gameObject.SetActive(true);
        currentMinigame.gameObject.SetActive(true);
        gameTimer = 0f;
    }
    void UpdateTimer()
    {
        gameTimer += Time.deltaTime;
        if (timerUI != null)
        {
            timerUI.text = GetGameTimerInFormat();
        }
    }
    public void PauseTimer()
    {
        pauseTimer = true;
        notification.SetActive(true);
    }
    public void UnPauseTimer()
    {
        pauseTimer = false;
        notification.SetActive(false);
    }
    public void EndMinigame()
    {
        timerCanvas?.gameObject.SetActive(false);
        currentMinigame.gameObject.SetActive(false);
        notification.SetActive(false);
        SetMinigame(MinigameType.None);
    }
    private void Update()
    {
        if (currentMinigame != null)
        {
            if (currentMinigameType != MinigameType.None
             && !IsCurrentActive())
            {
                StartMinigame();
            }

            if (currentMinigame.isCompleted)
            {
                EndMinigame();
            }
            else
            {
                if (!pauseTimer)
                {
                    // update timer?
                    UpdateTimer();
                }

            }
        }

    }
}
