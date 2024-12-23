using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearSlot : DropSlot
{
    public string requiredTag = "Big"; // Set this in the Inspector for each slot

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
                // Validate the tag of the gear piece
                if (gearPiece.CompareTag(requiredTag))
                {
                    BooleanGame.Instance.DisplayValidation("Correct Piece", 2f);
                }
                else
                {
                    BooleanGame.Instance.DisplayValidation("Incorrect Piece", 2f);
                }
            }
        }
    }
}
