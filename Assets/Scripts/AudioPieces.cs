using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AudioPieces : DragDrop
{
    private TMP_Text text;
    private string state; // Represents the state of the piece
    private Vector3 originalPosition; // To store the initial position
    private Transform originalParent; // To store the initial parent

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        originalPosition = transform.position;
        originalParent = transform.parent;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        Debug.Log($"Dropped piece with state: {state}");
    }

    public void SetText(string newText)
    {
        text.text = newText;
        state = newText; // Set state to match the text
    }

    public string GetState()
    {
        return state;
    }

    public void ResetPosition()
    {
        // Reset to the original position and parent
        transform.SetParent(originalParent);
        transform.position = originalPosition;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        //Debug.Log("Dragging " + gameObject.name);

    }
}
