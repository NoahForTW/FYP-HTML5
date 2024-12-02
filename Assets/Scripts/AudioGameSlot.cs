using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioGameSlot : MonoBehaviour, IDropHandler, IPointerUpHandler, IPointerDownHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // Reparent the dragged item to the slot and reset its position.
        RectTransform draggedItem = eventData.pointerDrag.GetComponent<RectTransform>();
        draggedItem.SetParent(transform);
        draggedItem.position = transform.position; // Align in the slot
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down pig");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up pig");
    }
}
