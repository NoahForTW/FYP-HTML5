using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioGameSlot : DropSlot
{
    public Image audioIcon;
    public string slotState; // Represents the expected state for this slot
    public bool isCorrect;

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);

        if (eventData.pointerDrag != null)
        {
            // Get the AudioPieces component from the dropped GamePiece
            AudioPieces droppedPiece = eventData.pointerDrag.GetComponent<AudioPieces>();

            if (droppedPiece != null)
            {
                string pieceState = droppedPiece.GetState();

                isCorrect = pieceState == slotState;
                // Validation: Check if the piece matches the slot
                if (pieceState == slotState)
                {
                    AudioGame.Instance.DisplayTextWithDelay("Correct Piece!", 2f);
                }
                else
                {
                    AudioGame.Instance.DisplayTextWithDelay("Incorrect Piece!", 2f);
                }
            }
            else
            {
                isCorrect = false;
            }
        }
    }
}
