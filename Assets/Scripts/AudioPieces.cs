using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioPieces : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private CanvasGroup canvasGroup; // For visual and interaction control
    private Transform originalParent; // Store the original parent to revert if needed

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false; // Allow drop detection
        transform.SetParent(transform.root); // Move to the top of the hierarchy for easy dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Re-enable interaction
        if (transform.parent == transform.root) // If dropped in an invalid area
        {
            transform.SetParent(originalParent); // Revert to the original parent
            transform.localPosition = Vector3.zero; // Reset position
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Optional: Add visual feedback like scaling or highlighting
    }
}
