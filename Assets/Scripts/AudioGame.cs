using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGame : MonoBehaviour
{
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

    [Header("Audio Game States")]
    [SerializeField] List<string> gameStates = new List<string>();
    
    void Start()
    {
        RandomiseStates();
        InitialisePiecesAndSlots();
    }

    // Shuffles the gameStates list
    void RandomiseStates()
    {
        for (int i = gameStates.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            string temp = gameStates[i];
            gameStates[i] = gameStates[randomIndex];
            gameStates[randomIndex] = temp;
        }
    }

    void InitialisePiecesAndSlots()
    {
        foreach (var state in gameStates)
        {
            // Instantiate GamePiece and set its text
            GameObject stateGO = Instantiate(GamePiece, GamePieceGroup.transform);
            stateGO.GetComponentInChildren<AudioPieces>().SetText(state);

            // Instantiate GameSlot and name it accordingly
            GameObject parent = Instantiate(GameSlot, GameSlotGroup.transform);
            parent.name = state + "Parent";

            Debug.Log("Initialised state: " + state);
        }
    }
}
