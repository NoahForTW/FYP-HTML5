using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public MinigameType MinigameType;
    public void StartMinigame()
    {
        MinigameManager.Instance.SetMinigame(MinigameType);
    }
}
