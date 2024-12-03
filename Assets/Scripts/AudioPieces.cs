using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioPieces : MonoBehaviour, IDragHandler, IPointerDownHandler, IDropHandler
{
    public Image image;

    public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent; // Allow drop detection
        transform.SetParent(transform.root); // Move to the top of the hierarchy for easy dragging
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true; // Re-enable interaction
        transform.SetParent(parentAfterDrag);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Optional: Add visual feedback like scaling or highlighting
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.TryGetComponent(out AudioPieces other))
        {
            Transform temp = transform.parent;
            transform.SetParent(other.parentAfterDrag);
            other.parentAfterDrag = temp;
        }
    }
}
