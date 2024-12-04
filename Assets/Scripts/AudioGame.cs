using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGame : MonoBehaviour
{
    [Tooltip("Assign a GamePiece Prefab")]
    [SerializeField] GameObject GamePiece;
    [Tooltip("Assign a GameSlot Prefab")]
    [SerializeField] GameObject GameSlot;

    [SerializeField] GameObject GamePieceGroup;
    [SerializeField] GameObject GameSlotGroup;

    [SerializeField] List<string> gameStates = new List<string>();
    
    void Start()
    {
        Print();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Print()
    {
        foreach (var state in gameStates)
        {

            // instantiate GO & set text to piece
            GameObject stateGO = Instantiate(GamePiece, GamePieceGroup.transform);
            stateGO.GetComponentInChildren<AudioPieces>().SetText(state.ToString());

            GameObject parent = Instantiate(GameSlot, GameSlotGroup.transform);
            parent.name = state + "parent";

            Debug.Log(state.ToString());

        }
    }
}
