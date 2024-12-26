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

        if (PlayerController.Instance.currentPlayerAction != PlayerAction.Interact
            &&Input.GetMouseButtonUp(0) && dialogueIsPlaying) {
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
            DisplayChoices();
        }
        else {
            ExitDialogueMode();
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
