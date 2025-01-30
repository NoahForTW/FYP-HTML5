using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMobilePlatformNPC : NPC
{
    public override void StartDialogue()
    {
        List<(string Name, object Value)> variableList = new List<(string Name, object Value)> {
            ("IsMobilePlatform", Application.isMobilePlatform)
        };  
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, variableList, null);
    }
}
