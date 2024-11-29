using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioPieces : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;
    private Vector3 originalPos; // Store the original position
    private Vector2 offset;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPos = transform.position; // Store the world position as original

        Debug.Log($"{gameObject.name} Original Position: {originalPos}");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Play SFX or trigger an event here
        isDragging = true;

        // Calculate offset between the pointer and the object's position
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var worldPoint);

        offset = (Vector2)(rectTransform.position - worldPoint);

        Debug.Log($"{gameObject.name} Parent: {transform.parent.name}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        Debug.Log($"{gameObject.name} Resetting to Original Position");
        // Reset position to the original position
        transform.position = originalPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
            return;

        // Update the object's position to follow the pointer while maintaining the offset
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var worldPoint))
        {
            rectTransform.position = worldPoint + (Vector3)offset;
        }
    }
}
