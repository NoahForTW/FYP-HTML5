using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioGameSlot : DropSlot
{
    public GameObject audioIcon;
    public string slotState; // Represents the expected state for this slot

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

                // Validation: Check if the piece matches the slot
                if (pieceState == slotState)
                {
                    AudioGame.Instance.DisplayTextWithDelay("Correct Piece!", 2f);

                    eventData.pointerDrag.transform.SetParent(transform);
                    eventData.pointerDrag.transform.localPosition = Vector3.zero;
                }
                else
                {
                    AudioGame.Instance.DisplayTextWithDelay("Incorrect Piece!", 2f);

                    // Reset the piece to its original position
                    droppedPiece.ResetPosition();
                }
            }
        }
    }
}
