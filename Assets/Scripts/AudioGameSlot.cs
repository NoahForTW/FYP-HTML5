using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioGameSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
         // Check if the dragged item is valid.
        if (eventData.pointerDrag != null)
        {
            // Reparent the dragged item to the slot and reset its position.
            RectTransform draggedItem = eventData.pointerDrag.GetComponent<RectTransform>();
            draggedItem.SetParent(transform);
            draggedItem.position = transform.position; // Align in the slot
        }
    }
}
