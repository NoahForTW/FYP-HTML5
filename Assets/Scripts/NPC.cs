using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public MinigameType MinigameType;

    public List<MinigameCompletedEffects> CompletedEffects;

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
        MinigameManager.Instance.MinigameCompletion.AddListener(MinigameCompleted);
    }

    public void StartDialogue()
    {
        canStartMinigame = CheckCompletionOfRequireMinigames();
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, () => StartMinigame());
    }
    void MinigameCompleted()
    {
        foreach (var effect in CompletedEffects)
        {
            if (effect == null)
                continue;

            switch (effect.completedEffects)
            {
                case global::CompletedEffects.Deactivate:
                    StartCoroutine(DeactivateGameObject(effect.gameObject, 2f));
                    break;
                case global::CompletedEffects.PlayAnimation:
                    effect.gameObject.GetComponent<Obstacle>()?.PlayAnimation();
                    break;
            }
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
    public CompletedEffects completedEffects;
}

public enum CompletedEffects
{
    None,
    Deactivate,
    PlayAnimation
}


