using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioGame : MonoBehaviour
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
    [SerializeField] private GameObject AudioGameParent;

    [Header("Audio Slot Images")]
    [SerializeField] private Sprite walk;
    [SerializeField] private Sprite jump;
    [SerializeField] private Sprite bgm;

    [Header("Audio Game States")]
    [SerializeField] List<string> gameStates = new List<string>();
    
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

    void InitialisePiecesAndSlots()
    {
        List<Sprite> slotImages = new List<Sprite> { walk, jump, bgm };

        if (slotImages.Count < gameStates.Count)
        {
            Debug.LogWarning("Not enough images for the number of game states!");
            return;
        }

        for (int i = 0; i < gameStates.Count; i++)
        {
            string state = gameStates[i];

            // Instantiate GamePiece and set its state
            GameObject stateGO = Instantiate(GamePiece, GamePieceGroup.transform);
            AudioPieces audioPieces = stateGO.GetComponentInChildren<AudioPieces>();
            audioPieces.SetText(state);
            audioPieces.parentDuringDrag = AudioGameParent.transform;

            // Instantiate GameSlot and assign its state and image
            GameObject slotGO = Instantiate(GameSlot, GameSlotGroup.transform);
            slotGO.name = state + "Parent";

            AudioGameSlot audioGameSlot = slotGO.GetComponentInChildren<AudioGameSlot>();
            if (audioGameSlot != null)
            {
                audioGameSlot.slotState = state; // Set the slot's expected state

                // Assign a unique image to the slot
                if (audioGameSlot.audioIcon != null)
                {
                    Image iconImage = audioGameSlot.audioIcon.GetComponent<Image>();
                    if (iconImage != null)
                    {
                        iconImage.sprite = slotImages[i];
                    }
                }
            }

            Debug.Log($"Initialised slot: {state} with image: {slotImages[i].name}");
        }
    }
}
