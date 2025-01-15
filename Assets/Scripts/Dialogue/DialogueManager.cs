using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using System.Globalization;

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
    private List<Action> bindActionNames;


    const string SpeakerTag = "speaker";
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Debug.LogWarning("More than 1 DialogueManager instance");
        }

        bindActionNames = new List<Action>();
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
        PlayerController.Instance.canMove = !dialogueIsPlaying;
        if (!dialogueIsPlaying) {
            return;
        }

        if (PlayerController.Instance.currentPlayerAction != PlayerAction.Interact
            &&Input.GetMouseButtonUp(0) && dialogueIsPlaying && !makingChoice) {
            ContinueStory();
        }
    }

    public void SetCurrentDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        
    }

    public void SetVariableInStory(string variableName, object variableValue)
    {
        if (currentStory == null)
            return;
        currentStory.variablesState[variableName] = variableValue;
    }

    public void BindFuntionToStory(Action action)
    {
        currentStory.BindExternalFunction(action.Method.Name, () =>
        {
            action.Invoke();
        });
    }
    public void UnbindFuntionToStory(Action action)
    {
        // unbind function
        currentStory.UnbindExternalFunction(action.Method.Name);
    }
    public void EnterDialogueMode(TextAsset inkJson, List<(string Name, object Value)> variables, List<Action> actions)
    {
        if (dialogueIsPlaying)
            return;
        if (inkJson == null)
            return;
        // reset bind list 
        bindActionNames.Clear();

        // set dialogue 
        SetCurrentDialogue(inkJson);

        // set variables if have
        if (variables != null && variables.Count > 0)
        {
            foreach (var (name, value) in variables)
            {
                SetVariableInStory(name, value);
            }
        }

        // set actions if have
        if (actions != null && actions.Count > 0)
        {
            foreach (var action in actions)
            {
                BindFuntionToStory(action);
                bindActionNames.Add(action);
            }
        }

        dialogueIsPlaying = true;

        // show panel
        dialoguePanel.SetActive(true);
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

        foreach(Action action in bindActionNames)
        {
            UnbindFuntionToStory(action);
        }
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
            HandleTags(currentStory.currentTags);
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

    void HandleTags(List<string> tags)
    {
        // reset text 
        speakerName.text = "";
        // set text
        foreach (string tag in tags)
        {
            // split tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                continue;
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SpeakerTag:
                    speakerName.text = tagValue;
                    break;
            }
        }
    }

}
