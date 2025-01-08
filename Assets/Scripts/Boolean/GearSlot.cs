using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearSlot : DropSlot
{
    public GameObject requiredGear;
    //public GearType requiredGearType; // The type of gear expected for this slot
    public bool isGearCorrect = false; // Tracks if the slot is correctly filled
    //private GearPiece currentGearPiece; // Reference to the piece in the slot

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
                // Validate the gear type
                //if (gearPiece.gearType == requiredGearType)
                isGearCorrect = droppedObject == requiredGear;
                string displayString = droppedObject == requiredGear ? "Correct Piece" : "Incorrect Piece";
                BooleanGame.Instance.DisplayValidation(displayString, 2f);
            }
        }
    }

    public bool IsGearCorrect()
    {
        return isGearCorrect;
    }

    //public void ClearSlot()
    //{
    //    if (currentGearPiece != null)
    //    {
    //        currentGearPiece.ResetPosition(); // Move the gear piece back to its original position
    //        currentGearPiece = null; // Clear reference
    //    }
    //    isGearPlaced = false; // Reset slot state
    //}
}
