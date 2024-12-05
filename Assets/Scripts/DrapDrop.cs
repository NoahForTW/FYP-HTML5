using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    protected RectTransform rectTransform; 
    protected Canvas canvas;               
    protected CanvasGroup canvasGroup;     
    public Camera canvasCamera;            
    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasCamera = canvas.worldCamera;
    }

    // Triggered when dragging begins
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false; 
    }

    // Triggered while dragging
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            var screenPoint = Input.mousePosition;
            screenPoint.z = canvas.planeDistance; //distance of the plane from the camera
            transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        }
    }

    // Triggered when dragging ends
    public virtual void OnEndDrag(PointerEventData eventData)
    {

        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}

