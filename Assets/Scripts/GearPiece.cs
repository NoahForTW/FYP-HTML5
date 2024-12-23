using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;

public class GearPiece : DragDrop
{
    private Transform originalParent;
    private Vector3 originalPosition;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;

        parentDuringDrag = BooleanGame.Instance.GearGameParent.transform;
    }

    public bool ValidatePiece()
    {
        if (CompareTag("Big")) // Assuming "Big" is the correct tag
        {
            return true; // Correct piece
        }
        return false; // Incorrect piece
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
