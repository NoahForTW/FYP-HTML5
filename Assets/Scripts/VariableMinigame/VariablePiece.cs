using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class VariablePiece : DragDrop
{
    TMP_Text text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        parentDuringDrag = VariableMinigame.Instance.slotsPieceParent.transform;
       
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
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        transform.position = parentAfterDrag.position;
        transform.SetParent(parentAfterDrag);
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }

    public string GetText()
    {
        return text.text;
    }
}
