using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public MinigameType MinigameType;

    public List<MinigameCompletedEffects> CompletedEffects;
    public void StartMinigame()
    {
        MinigameManager.Instance.SetMinigame(MinigameType);
        MinigameManager.Instance.minigameCompletion.AddListener(MinigameCompleted);
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


