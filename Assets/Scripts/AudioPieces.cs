using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AudioPieces : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image image;
    private TMP_Text text;
    private CanvasGroup canvasGroup;

    [HideInInspector] public Transform parentAfterDrag;

    public bool isInSlot = false;

    private void Awake() 
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        parentAfterDrag = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
         // Allow drop detection
        //transform.SetParent(transform.root); // Move to the top of the hierarchy for easy dragging
        //transform.SetAsLastSibling();
        //image.raycastTarget = false;
        canvasGroup.blocksRaycasts = false;
        Debug.Log("Picked " + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

        Debug.Log("Dragging " + gameObject.name);
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        //image.raycastTarget = true; // Re-enable interaction
        if(!isInSlot)
        {
            transform.SetParent(parentAfterDrag);
            transform.position = parentAfterDrag.position;
        }
        canvasGroup.blocksRaycasts = true;
        Debug.Log("Stop Dragging " + gameObject.name);
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }




}
