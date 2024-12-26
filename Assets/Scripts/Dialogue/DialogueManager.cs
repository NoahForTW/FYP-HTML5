using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerName;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Story Dialogue")]
    public bool dialogueIsPlaying;
    private Story currentStory;
    private bool makingChoice;

    private const string SPEAKER_TAG = "speaker";

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Debug.LogWarning("More than 1 DialogueManager instance");
        }
    }

    public static DialogueManager GetInstance() {
        return instance;
    }

    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        for (int i = 0; i < choices.Length; i++) {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }


    void Update()
    {
        if (!dialogueIsPlaying) {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !makingChoice) {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON) {
        if (dialogueIsPlaying) {
            return;
        }
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    private void ExitDialogueMode() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory() {
        if (currentStory.canContinue) {
            dialogueText.text = currentStory.Continue();
            HandleTags(currentStory.currentTags);
            DisplayChoices();
        }
        else {
            ExitDialogueMode();
        }
    }

    private void HandleTags(List<string> currentTags) {
        
        foreach (string tag in currentTags) {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) {
                Debug.LogError("Tag could not be appropriately parsed" + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch(tagKey) {
                case SPEAKER_TAG:
                    speakerName.text = tagValue;
                    break;
                default:
                    Debug.LogWarning("This tag is currently not supported");
                    break;
            }
        }

    }

    private void DisplayChoices() {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length) {
            Debug.LogError("more choices given than UI can support");
        }

        int index = 0;

        for (int i = 0; i < currentChoices.Count; i++) {
            choices[i].gameObject.SetActive(true);
            choicesText[i].text = currentChoices[i].text;
            index++;
        }
        
        for (int i = index; i < choices.Length; i++) {
            choices[i].gameObject.SetActive(false);
        }

        if (currentStory.currentChoices.Count != 0) {
            makingChoice = true;
        }
    }

    public void MakeChoice(int choiceIndex) {
        currentStory.ChooseChoiceIndex(choiceIndex);
        makingChoice = false;
        ContinueStory();
    }
}
