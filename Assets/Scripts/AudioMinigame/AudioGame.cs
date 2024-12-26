using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioGame : Minigame
{
    public static AudioGame Instance;

    [Header("Assign the Audio prefab here")]
    [Tooltip("Assign a GamePiece Prefab")]
    [SerializeField] GameObject GamePiece;
    [Tooltip("Assign a GameSlot Prefab")]
    [SerializeField] GameObject GameSlot;

    [Header("Assign the Audio groups here")]
    [Tooltip("Assign a GamePiece Parent")]
    [SerializeField] GameObject GamePieceGroup;
    [Tooltip("Assign a GameSlot Parent")]
    [SerializeField] GameObject GameSlotGroup;

    [Tooltip("Assign a GameSlot Parent")]
    public GameObject AudioGameParent;

    [Header("Audio Slot Images")]
    [SerializeField] private Sprite walk;
    [SerializeField] private Sprite jump;
    [SerializeField] private Sprite bgm;

    [Header("Validation Text")]
    public TMP_Text audioValidText;

    [Header("Audio Game States")]
    [SerializeField] List<AudioGame_SO> GameQuestions = new List<AudioGame_SO>();

    List<AudioGameSlot> AudioSlots = new List<AudioGameSlot>();

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }
    
    void Start()
    {
        InitialisePiecesAndSlots();
    }

    public void DisplayTextWithDelay(string message, float delay)
    {
        StopAllCoroutines(); // Stop any ongoing coroutine to avoid overlapping
        audioValidText.text = message;
        audioValidText.gameObject.SetActive(true); // Ensure the text is visible
        StartCoroutine(HideTextAfterDelay(delay));
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioValidText.gameObject.SetActive(false); // Hide the text after the delay
    }

    void InitialisePiecesAndSlots()
    {
/*        List<Sprite> slotImages = new List<Sprite> { 
            walk, 
            jump, 
            bgm 
        };

        if (slotImages.Count < gameQuestions.Count)
        {
            Debug.LogWarning("Not enough images for the number of game states!");
            return;
        }*/
        AudioSlots.Clear();

        for (int i = 0; i < GameQuestions.Count; i++)
        {
            AudioGame_SO gameQuestion = GameQuestions[i];
            string audioName = GameQuestions[i].AudioName;

            // Instantiate GamePiece and set its state
            GameObject stateGO = Instantiate(GamePiece, GamePieceGroup.transform);
            AudioPieces audioPieces = stateGO.GetComponentInChildren<AudioPieces>();
            audioPieces.SetText(audioName);
            audioPieces.parentDuringDrag = AudioGameParent.transform;

            // Instantiate GameSlot and assign its state and image
            GameObject slotGO = Instantiate(GameSlot, GameSlotGroup.transform);
            slotGO.name = audioName + "Parent";
            AudioGameSlot audioGameSlot = slotGO.GetComponentInChildren<AudioGameSlot>();
            if (audioGameSlot != null)
            {
                audioGameSlot.slotState = audioName; // Set the slot's expected state
                AudioSlots.Add(audioGameSlot);
                // Assign a unique image to the slot
                if (audioGameSlot.audioIcon != null)
                {
                    audioGameSlot.audioIcon.sprite = gameQuestion.AudioSprite;
                }
            }
            

            //Debug.Log($"Initialised slot: {state} with image: {slotImages[i].name}");
        }
    }

    bool AllSlotsAreCorrect()
    {
        foreach(AudioGameSlot slot in AudioSlots)
        {
            if (!slot.isCorrect)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (AllSlotsAreCorrect())
        {
            isCompleted = true;
            Debug.Log("Completed");
        }
    }
}
