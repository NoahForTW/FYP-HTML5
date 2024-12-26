using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public void TriggerDialogue(TextAsset inkJSON) {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
}
