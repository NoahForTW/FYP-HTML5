using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0)
        {
            foreach (Transform child in transform)
            {
                DragDrop pieces = child.GetComponent<DragDrop>();

                if (pieces == null) continue;

                Transform parent = pieces.parentAfterDrag;
                pieces.parentSlot = null;
                // Smoothly return existing child to its original parent
                StartCoroutine(pieces.SmoothMove(child.position, parent.position, 0.8f, () =>
                {
                    child.SetParent(parent);
                }));
            }
        }

        GameObject dropped = eventData.pointerDrag;
/*        DragDrop draggableItem = dropped.GetComponent<DragDrop>();
        draggableItem.parentSlot = transform;*/

        SetThisToParentSlot(dropped);
        // Smoothly snap the dropped object into the slot
/*        StartCoroutine(draggableItem.SmoothMove(dropped.transform.position, transform.position, 0.8f, () =>
        {
            dropped.transform.SetParent(transform);
        }));*/
    }

    public void SetThisToParentSlot(GameObject dragged)
    {
        DragDrop draggableItem = dragged.GetComponent<DragDrop>();
        draggableItem.parentSlot = transform;

    }
}
