using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.Rendering;
using System;
using UnityEngine.SceneManagement;

public enum MinigameType
{
    None,
    Audio,
    Variable_Gear,
    Variable_Type,
    Variable_Wires,
    Drawing,
    ModelTexture,
    Frames,
}

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    [Header("Current Minigame")]
    Minigame CurrentMinigame;
    MinigameType CurrentMinigameType;
    public List<ScriptableObject> CurrentQuestions;

    public UnityEvent MinigameCompletion;
    public UnityEvent MinigameFailed;

    //minigame list
    List<Minigame> Minigames = new List<Minigame>();

    // timer UI & variables
    TMP_Text TimerUI;
    GameObject Notification;

    float GameTimer;
    bool PauseTime = false;

    // clue cost
    float baseClueCost = 10;
    int ClueCostMultiplier = 1;
    public UnityEvent clueCostUpdated;
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
        Notification = CanvasManager.Instance.NotificationCanvas?.notification;

        if (CanvasManager.Instance.TimerCanvas != null)
        {
            CanvasManager.Instance.TimerCanvas.gameObject.SetActive(false);
        }
    }

    public void SetTimer(float time)
    {
        GameTimer = time;
    }
    public void SetMinigame(MinigameType type)
    {
        CurrentMinigameType = type;
        if (type == MinigameType.None)
        {
            CurrentMinigame = null;
            return;
        }

        
        foreach(var game in Minigames)
        {
            if (game.minigameType == type)
            {
                CurrentMinigame = game;
                break;
            }
        }
    }

    public void SetQuestions(List<ScriptableObject> questions)
    {
        CurrentQuestions = questions;
    }

    public List<ScriptableObject> GetQuestions()
    {
        return CurrentQuestions;
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
        CanvasManager.Instance.GUICanvas.CanvasGroup.blocksRaycasts = false;
        CanvasManager.Instance.GUICanvas.SetActiveControlsUI(false);
        CurrentMinigame.StartMinigame();
        PlayerController.Instance.canMove = false;
    }
    void UpdateTimer()
    {
        GameTimer -= Time.deltaTime;
        if (TimerUI != null)
        {
            TimerUI.text = GetGameTimerInFormat();
        }
    }
    public void PauseTimer()
    {
        PauseTime = true;
        CanvasManager.Instance.NotificationCanvas?.SetPauseNotif();

    }
    public void UnPauseTimer()
    {
        PauseTime = false;
        Notification.SetActive(false);
    }
    public void EndMinigame()
    {
        CanvasManager.Instance.TimerCanvas?.gameObject.SetActive(false);
        CanvasManager.Instance.GUICanvas.CanvasGroup.blocksRaycasts = true;
        CurrentMinigame.EndMinigame();
        CanvasManager.Instance.GUICanvas.SetActiveControlsUI(true);
        Notification.SetActive(false);
        SetMinigame(MinigameType.None);
        PlayerController.Instance.canMove = true;
    }
    void ShowResults()
    {
        PauseTime = true;
        int coinsEarned = (int)((GameTimer / CurrentMinigame.maxTimeInSeconds)* 25);
        CanvasManager.Instance.NotificationCanvas?.SetGameDoneNotif(GetGameTimerInFormat(), coinsEarned);
        // addd & save coins
        PlayerInventory.Instance.AddCoins(coinsEarned);
    }

    public void SetClueMultiplier(int newMultiplier)
    {
        ClueCostMultiplier = newMultiplier;
        clueCostUpdated.Invoke();
    }

    public int GetCurrentClueCost()
    {
        return (int)(ClueCostMultiplier * baseClueCost);
    }
    public void ShowClue(string clue)
    {
        int cost = GetCurrentClueCost();
        if (PlayerInventory.Instance.GetCurrentCoins() >= cost)
        {
            CanvasManager.Instance.NotificationCanvas?.SetWindowNotif(clue);
            PlayerInventory.Instance.RemoveCoins(cost);
            SetClueMultiplier(ClueCostMultiplier + 1);
        }
        else
        {
            CanvasManager.Instance.NotificationCanvas?.StartTextNotif("You have not enough coins!");
        }
    }
    public void ClearChild(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    void SaveMinigame(MinigameType type)
    {

    }
    private void Update()
    {
        if (CurrentMinigame == null)
            return;
        if (CurrentMinigameType != MinigameType.None
            && !IsCurrentActive())
        {
            StartMinigame();
        }

        if (CurrentMinigame.isCompleted)
        {
            //EndMinigame();
            MinigameCompletion.Invoke();
            // show result notification
            ShowResults();
        }
        else
        {
            if (!PauseTime)
            {
                // update timer?
                UpdateTimer();

                if (GameTimer <=0)
                {
                    EndMinigame();
                }
            }

        }
        

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Minigames = CanvasManager.Instance.GetComponentsInChildren<Minigame>(true).ToList();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelData data = SceneManager.GetActiveScene().name == "GDTLevel"
        ? SavePlayerData.Instance.LoadData<GDTLevelData>()
        : SavePlayerData.Instance.LoadData<AGVELevelData>();

        
    }
}
