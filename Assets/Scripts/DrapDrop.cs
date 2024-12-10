using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    protected RectTransform rectTransform; 
    protected Canvas canvas;               
    protected CanvasGroup canvasGroup;     
    public Camera canvasCamera;

    public Transform parentAfterDrag;
    public bool isInSlot = false;
    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasCamera = canvas.worldCamera;
        parentAfterDrag = transform.parent;
    }

    // Triggered when dragging begins
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        if (GetComponent<Image>() != null)
            GetComponent<Image>().maskable = false;
        if (GetComponent<RawImage>() != null)
            GetComponent<RawImage>().maskable = false;
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
            Vector3 dragPos = new Vector3(Camera.main.ScreenToWorldPoint(screenPoint).x,
                Camera.main.ScreenToWorldPoint(screenPoint).y, transform.position.z); 
            transform.position = dragPos;
        }
    }

    // Triggered when dragging ends
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        Debug.Log(GetComponent<RawImage>().maskable);
        if (GetComponent<Image>() != null)
            GetComponent<Image>().maskable = true;
        if (GetComponent<RawImage>() != null)
            GetComponent<RawImage>().maskable = true;

        if (!isInSlot)
        {
            // Smoothly return the object to its original position
            StartCoroutine(SmoothMove(transform.position, parentAfterDrag.position, 0.8f, () =>
            {
                transform.SetParent(parentAfterDrag);
            }));
        }
    }

    public IEnumerator SmoothMove(Vector3 start, Vector3 end, float duration, System.Action onComplete = null)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end; // Snap to the final position
        onComplete?.Invoke();
    }
}

