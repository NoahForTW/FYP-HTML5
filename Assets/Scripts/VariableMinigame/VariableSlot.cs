using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
                isPieceCorrect(piece.GetText() == letter.ToString(), piece.gameObject);
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

    void isPieceCorrect(bool isPieceCorrect, GameObject piece)
    {
        isCorrect = isPieceCorrect;
        // set piece color
        Color pieceColor = isPieceCorrect? Color.green : Color.red;

        piece.GetComponentInChildren<TextMeshProUGUI>().color = pieceColor;
        
    }
}
