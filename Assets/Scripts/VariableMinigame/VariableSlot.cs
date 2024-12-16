using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VariableSlot : DropSlot
{
    public char letter;
    public bool isCorrect = false;

    private void Update()
    {
        if (transform.childCount != 0)
        {
            Transform child = transform.GetChild(0);
            VariablePiece piece = child.GetComponentInChildren<VariablePiece>();
            if (piece != null)
            {
                isCorrect = piece.GetText() == letter.ToString();
            }
        }
        else
        {
            isCorrect = false;
        }
    }
    public override void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        GameObject item = eventData.pointerDrag;
        DragDrop draggableItem = item.GetComponent<DragDrop>();
        if (item.GetComponent<DragDrop>()!= null)
        {
            GameObject child = Instantiate(item, transform);
            child.transform.localPosition = Vector3.zero;
        }
    }
}
