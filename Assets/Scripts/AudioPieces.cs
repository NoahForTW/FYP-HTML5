using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioPieces : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;

    private Vector2 offset, originalPos;

    private RectTransform rectTransform;

    private void Awake()
    {
        originalPos =   transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDragging) 
        {
            return;
        }

        var mousePosition = GetPointerPos();

        transform.position = mousePosition - offset;
    }

    private void OnPointerDown()
    {
        // can play sfx here

        isDragging = true;

        offset = GetPointerPos() - (Vector2)transform.position;
    }

    private void OnPointerUp()
    {
        transform.position = originalPos;
        isDragging = false;
    }

    private Vector2 GetPointerPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
