using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioGameSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Slot name: " + transform.parent.gameObject.name);

        if (transform.childCount != 0)
        {
            foreach (Transform child in transform)
            {
                AudioPieces audioPieces = child.GetComponent<AudioPieces>();

                if (audioPieces == null) continue;

                Transform parent = audioPieces.parentAfterDrag;
                audioPieces.isInSlot = false;

                AudioManager.PlaySoundOneShot(SoundType.Drag);
                // Smoothly return existing child to its original parent
                StartCoroutine(audioPieces.SmoothMove(child.position, parent.position, 0.8f, () =>
                {
                    child.SetParent(parent);
                }));
            }
        }

        GameObject dropped = eventData.pointerDrag;
        AudioPieces draggableItem = dropped.GetComponent<AudioPieces>();
        draggableItem.isInSlot = true;

        AudioManager.PlaySoundOneShot(SoundType.Drag);
        // Smoothly snap the dropped object into the slot
        StartCoroutine(draggableItem.SmoothMove(dropped.transform.position, transform.position, 0.8f, () =>
        {
            dropped.transform.SetParent(transform);
        }));

        //Debug.Log("Dropped " + draggableItem.gameObject);
    }
}
