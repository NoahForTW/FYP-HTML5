using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioGameSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("bitch" + transform.parent.gameObject.name);
        if (transform.childCount != 0) //when there is shit in child
        {
            foreach(Transform child in transform)
            {
                AudioPieces audioPieces = child.GetComponent<AudioPieces>();

                if (audioPieces == null) { continue; }
                Transform parent = audioPieces.parentAfterDrag;
                audioPieces.isInSlot = false;
                child.SetParent(parent);
                child.position = parent.position;
            }
        }
        
        GameObject dropped = eventData.pointerDrag;
        AudioPieces draggableItem = dropped.GetComponent<AudioPieces>();
        draggableItem.isInSlot = true;
        dropped.transform.SetParent(transform);
        dropped.transform.position = transform.position;

        Debug.Log("Dropped " + draggableItem.gameObject);

    }
}
