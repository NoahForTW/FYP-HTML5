using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class VariablePiece : DragDrop
{
    TMP_Text text;
    private void Start()
    {
        parentDuringDrag = VariableMinigame.Instance.variablePieceParent.transform;
        text = GetComponentInChildren<TMP_Text>();
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }

}
