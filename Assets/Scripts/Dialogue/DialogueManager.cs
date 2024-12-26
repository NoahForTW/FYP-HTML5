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

    [Header("Cameras")]
    [SerializeField] private GameObject[] cameras;

    [Header("Inventory")]
    [SerializeField] private GameObject inventoryPanel;
    private bool openInventory = false;

    [Header("Story Dialogue")]
    public bool dialogueIsPlaying;
    private Story currentStory;
    private bool makingChoice;

    private const string SPEAKER_TAG = "speaker";
    private const string CAMERA_TAG = "camera";
    private const string INVENTORY_TAG = "inventory";

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

        inventoryPanel.SetActive(false);

        foreach (GameObject camera in cameras) {
            camera.SetActive(false);
        }
        cameras[0].SetActive(true);
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
        inventoryPanel.SetActive(false);
        ContinueStory();
    }

    private void ExitDialogueMode() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        inventoryPanel.SetActive(openInventory);

        //you can omit this part if it doesnt fit the story
        foreach (GameObject camera in cameras) {
            camera.SetActive(false);
        }
        cameras[0].SetActive(true);
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
                case CAMERA_TAG:
                    foreach (GameObject camera in cameras) {
                        camera.SetActive(false);
                        if (camera.name == tagValue) {
                            camera.SetActive(true);
                        }
                    }
                    break;
                case INVENTORY_TAG:
                    if (tagValue == "open") {
                        openInventory = true;
                    }
                    else if (tagValue == "close") {
                        openInventory = false;
                    }
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
