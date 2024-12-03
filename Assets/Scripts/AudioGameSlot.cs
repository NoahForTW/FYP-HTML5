using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioGameSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            AudioPieces draggableItem = dropped.GetComponent<AudioPieces>();
            draggableItem.parentAfterDrag = transform;
        }
    }
}
