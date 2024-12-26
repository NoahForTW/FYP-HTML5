using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerName;

    [Header("Choices UI")]
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private GameObject choiceParent;
    List<Transform> choiceList = new List<Transform>();

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
    }


    void Update()
    {
        if (!dialogueIsPlaying) {
            return;
        }

        if (PlayerController.Instance.currentPlayerAction != PlayerAction.Interact
            &&Input.GetMouseButtonUp(0) && dialogueIsPlaying && !makingChoice) {
            ContinueStory();
        }
    }
    public void EnterDialogueMode(TextAsset inkJSON, Action action) {
        if (dialogueIsPlaying) {
            return;
        }
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        // add functions to ink
        currentStory.BindExternalFunction("StartMinigame", () =>
        {
            action.Invoke();
            //Debug.Log("Start Minigame");
        });
        ContinueStory();

        // hide player controls
        CanvasManager.Instance.GUICanvas.PlayerControlsUI.SetActive(false);


    }

    private void ExitDialogueMode() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        // hide player controls
        CanvasManager.Instance.GUICanvas.PlayerControlsUI.SetActive(true);

        // unbind function
        currentStory.UnbindExternalFunction("StartMinigame");
    }

    private void ContinueStory() {
        if (currentStory.canContinue) {
            string nextLine = currentStory.Continue();
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                ExitDialogueMode();
            }
            dialogueText.text = nextLine;
            DisplayChoices();
        }
        else {
            ExitDialogueMode();
        }
    }


    private void DisplayChoices() {
        List<Choice> currentChoices = currentStory.currentChoices;
        foreach(Transform child in choiceList)
        {
            Destroy(child.gameObject);
        }
        choiceList.Clear();
        if (currentChoices.Count <= 0) {
            return;
        }

        makingChoice = true;
        
        for (int i = 0; i < currentChoices.Count; i++) {
            GameObject choice = Instantiate(choicePrefab, choiceParent.transform);
            choice.GetComponentInChildren<TextMeshProUGUI>().text = currentChoices[i].text;
            int index = i;
            choice.GetComponent<Button>().onClick.AddListener(()=> MakeChoice(index));
            choiceList.Add(choice.transform);
        }
    }

    public void MakeChoice(int choiceIndex) {
        currentStory.ChooseChoiceIndex(choiceIndex);
        makingChoice = false;
        ContinueStory();
    }
}
