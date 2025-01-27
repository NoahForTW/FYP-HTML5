using Ink.Runtime;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Dialogue File")]
    public TextAsset inkJSON;

    public TextMeshProUGUI NPCNameText;

    Story NPCStory;
    private void Awake()
    {
        if (NPCNameText == null)
            return;
        NPCStory = new Story(inkJSON.text);
        NPCNameText.text = NPCStory.variablesState["NPCName"].ToString();
    }
    public virtual void StartDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, null, null);
    }
}
