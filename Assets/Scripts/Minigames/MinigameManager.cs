using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Linq;

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

    Minigame CurrentMinigame;
    MinigameType CurrentMinigameType;

    public UnityEvent MinigameCompletion;

    //minigame list
    List<Minigame> Minigames = new List<Minigame>();

    // timer UI & variables
    TMP_Text TimerUI;
    GameObject Notification;

    float GameTimer;
    bool PauseTime = false;
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
        TimerUI = CanvasManager.Instance.TimerCanvas?.timerText;
        Notification = CanvasManager.Instance.TimerCanvas?.notification;

        if (CanvasManager.Instance.TimerCanvas != null)
        {
            CanvasManager.Instance.TimerCanvas.gameObject.SetActive(false);
        }
    }
    public void SetMinigame(MinigameType type)
    {
        CurrentMinigameType = type;
        if (type == MinigameType.None)
        {
            CurrentMinigame = null;
            return;
        }

        Minigames = CanvasManager.Instance.GetComponentsInChildren<Minigame>(true).ToList();
        foreach(var game in Minigames)
        {
            if (game.minigameType == type)
            {
                CurrentMinigame = game;
                break;
            }
        }
    }

    public MinigameType GetCurrentMinigameType()
    {
        return CurrentMinigameType;
    }

    public Minigame GetCurrentMinigame()
    {
        return CurrentMinigame;
    }

    public string GetGameTimerInFormat()
    {
        int min = Mathf.FloorToInt(GameTimer / 60);
        int seconds = Mathf.FloorToInt(GameTimer % 60);
        return string.Format("{0:00}:{1:00}", min, seconds);
    }

    public bool CheckIfMinigameCompleted(MinigameType minigameType)
    {
        foreach (var game in Minigames)
        {
            if (game.minigameType == minigameType && game.isCompleted)
            {
                return true;
            }
        }
        return false;
    }

    bool IsCurrentActive()
    {
        if (CurrentMinigame == null)
            return false;

        return CurrentMinigame.gameObject.activeSelf;
    }

    void StartMinigame()
    {
        PauseTime = false;
        CanvasManager.Instance.TimerCanvas?.gameObject.SetActive(true);
        CurrentMinigame.gameObject.SetActive(true);
        GameTimer = 0f;
    }
    void UpdateTimer()
    {
        GameTimer += Time.deltaTime;
        if (TimerUI != null)
        {
            TimerUI.text = GetGameTimerInFormat();
        }
    }
    public void PauseTimer()
    {
        PauseTime = true;
        Notification.SetActive(true);
    }
    public void UnPauseTimer()
    {
        PauseTime = false;
        Notification.SetActive(false);
    }
    public void EndMinigame()
    {
        CanvasManager.Instance.TimerCanvas?.gameObject.SetActive(false);
        CurrentMinigame.gameObject.SetActive(false);
        Notification.SetActive(false);
        SetMinigame(MinigameType.None);
    }
    private void Update()
    {
        if (CurrentMinigame != null)
        {
            if (CurrentMinigameType != MinigameType.None
             && !IsCurrentActive())
            {
                StartMinigame();
            }

            if (CurrentMinigame.isCompleted)
            {
                EndMinigame();
                MinigameCompletion.Invoke();
            }
            else
            {
                if (!PauseTime)
                {
                    // update timer?
                    UpdateTimer();
                }

            }
        }

    }
}
