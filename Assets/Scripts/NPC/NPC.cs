using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Dialogue File")]
    public TextAsset inkJSON;
    
    public virtual void StartDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, null, null);
    }
}
