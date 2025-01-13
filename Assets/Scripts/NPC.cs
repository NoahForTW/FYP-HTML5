using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool IsMinigameCompleted = false;
    public MinigameType MinigameType;

    public List<Obstacle> CompletedEffects;
    
    [Header("Questions")]
    public List<ScriptableObject> questions;

    [Header("Required Completed Minigames")]
    [SerializeField] private List<MinigameType> CompletedMinigames;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    bool canStartMinigame = false;
    public void StartMinigame()
    {
        if (!canStartMinigame)
            return;
        MinigameManager.Instance.SetMinigame(MinigameType);
        MinigameManager.Instance.SetQuestions(questions);
        MinigameManager.Instance.MinigameCompletion.AddListener(MinigameCompleted);
    }

    public void StartDialogue()
    {
        canStartMinigame = CheckCompletionOfRequireMinigames();
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, () => StartMinigame());
    }
    void MinigameCompleted()
    {
        DialogueManager.GetInstance().SetVariable("isMinigameDone", true);
        IsMinigameCompleted = true;
        foreach (var effect in CompletedEffects)
        {
            if (effect == null || effect.gameObject == null)
                continue;
            effect.Event.Invoke();
        }
        MinigameManager.Instance.MinigameCompletion.RemoveListener(MinigameCompleted);
    }

    bool CheckCompletionOfRequireMinigames()
    {
        foreach(MinigameType type in CompletedMinigames)
        {
            if (!MinigameManager.Instance.CheckIfMinigameCompleted(type))
            {
                return false;
            }
        }

        return true;
    }
    IEnumerator DeactivateGameObject(GameObject go, float duration)
    {
        yield return new WaitForSeconds(duration);
        go.SetActive(false);
    }
}

[Serializable]
public class MinigameCompletedEffects
{
    public GameObject gameObject;
    public CompletionEffects completedEffects;
}



