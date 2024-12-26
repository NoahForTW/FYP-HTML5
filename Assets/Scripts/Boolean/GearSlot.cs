using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearSlot : DropSlot
{
    public GearType requiredGearType; // The type of gear expected for this slot
    public bool isGearPlaced; // Tracks if the slot is correctly filled
    private GearPiece currentGearPiece; // Reference to the piece in the slot

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);

        // Check if the object being dropped is a GearPiece
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            GearPiece gearPiece = droppedObject.GetComponent<GearPiece>();
            if (gearPiece != null)
            {
                // Check if slot already contains a gear piece
                if (isGearPlaced)
                {
                    BooleanGame.Instance.DisplayValidation("Slot already filled!", 2f);
                    return;
                }

                // Validate the gear type
                if (gearPiece.gearType == requiredGearType)
                {
                    BooleanGame.Instance.DisplayValidation("Correct Piece", 2f);
                    isGearPlaced = true; // Mark slot as filled
                    currentGearPiece = gearPiece; // Store reference to the piece
                }
                else
                {
                    BooleanGame.Instance.DisplayValidation("Incorrect Piece", 2f);
                }
            }
        }
    }

    public bool IsSlotEmpty()
    {
        return !isGearPlaced;
    }

    public void ClearSlot()
    {
        if (currentGearPiece != null)
        {
            currentGearPiece.ResetPosition(); // Move the gear piece back to its original position
            currentGearPiece = null; // Clear reference
        }
        isGearPlaced = false; // Reset slot state
    }
}
