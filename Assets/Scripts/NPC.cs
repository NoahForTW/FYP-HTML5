using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public MinigameType MinigameType;

    public List<MinigameCompletedEffects> CompletedEffects;

    [Header("Required Completed Minigames")]
    [SerializeField] private List<MinigameType> completedMinigames;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    public void StartMinigame()
    {
        MinigameManager.Instance.SetMinigame(MinigameType);
        MinigameManager.Instance.minigameCompletion.AddListener(MinigameCompleted);
    }

    public void StartDialogue()
    {
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
            }
        }
        MinigameManager.Instance.minigameCompletion.RemoveListener(MinigameCompleted);
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


