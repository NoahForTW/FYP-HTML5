using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameNPC : NPC
{
    public bool IsMinigameCompleted = false;
    public MinigameType MinigameType;

    [Header("Sprite")]
    [SerializeField] SpriteRenderer spriteImage;
    [SerializeField] Sprite Gremlin;
    Sprite NPCSprite;

    public List<Obstacle> CompletedEffects;
    
    [Header("Questions")]
    public List<ScriptableObject> questions;

    [Header("Required Completed Minigames")]
    [SerializeField] private List<MinigameType> CompletedMinigames;

    bool canStartMinigame = false;

    public override void Awake()
    {
        base.Awake();
        if (spriteImage != null)
            NPCSprite = spriteImage.sprite;
    }
    public void StartMinigame()
    {
        MinigameManager.Instance.SetMinigame(MinigameType);
        MinigameManager.Instance.SetQuestions(questions);
        MinigameManager.Instance.MinigameCompletion.AddListener(MinigameCompleted);
        MinigameManager.Instance.MinigameFailed.AddListener(MinigameFailed);
    }

    public override void StartDialogue()
    {
        // get from playerdata foir mingame
        //IsMinigameCompleted = PLayerPrefs
        List<(string Name, object Value)> variableList = new List<(string Name, object Value)> {
            (nameof(IsMinigameCompleted), IsMinigameCompleted)
        };
        List<Action> actionList = new List<Action> { StartMinigame, ChangeAvatar };
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, variableList, actionList);
    }
    void MinigameCompleted()
    {
        IsMinigameCompleted = true;
        DialogueManager.GetInstance().SetVariableInStory(nameof(IsMinigameCompleted), IsMinigameCompleted);
        foreach (var effect in CompletedEffects)
        {
            if (effect == null || effect.gameObject == null)
                continue;
            effect.Event.Invoke();
        }
        MinigameManager.Instance.MinigameCompletion.RemoveListener(MinigameCompleted);
    }

    void MinigameFailed()
    {
        DialogueManager.GetInstance().SetVariableInStory("IsMinigameFailed", true);
    }

    void ChangeAvatar()
    {
        if (spriteImage == null || Gremlin == null)
            return;

        spriteImage.sprite = spriteImage.sprite == Gremlin ? NPCSprite : Gremlin;
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



