using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;

public enum GearType
{
    None,
    Big,
    Small
}

public class GearPiece : DragDrop
{
    public GearType gearType;
    private Transform originalParent;
    private Vector3 originalPosition;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        parentDuringDrag = BooleanGame.Instance.GearGameParent.transform;
        //originalPosition = transform.position;
        //originalParent = transform.parent;
    }

    public bool ValidatePiece(GearType expectedType)
    {
        return gearType == expectedType;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        //originalPosition = transform.position;
        //originalParent = transform.parent;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
    }

    public void ResetPosition()
    {
        StartCoroutine(SmoothMove(transform.position, parentAfterDrag.position, 0.8f, () =>
        {
            transform.SetParent(parentAfterDrag);
        }));
        // Reset to the original position and parent
/*        transform.SetParent(parentAfterDrag);
        transform.position = originalPosition;*/
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        //Debug.Log("Dragging " + gameObject.name);

    }
}
