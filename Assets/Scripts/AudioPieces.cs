using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AudioPieces : DragDrop, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private TMP_Text text;
    private CanvasGroup canvasGroup;

    [HideInInspector] public Transform parentAfterDrag;

    public bool isInSlot = false;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();
        parentAfterDrag = transform.parent;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        AudioManager.PlaySoundOneShot(SoundType.Drag);
        //Debug.Log("Picked " + gameObject.name);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        AudioManager.PlaySoundOneShot(SoundType.Drag);
        //Debug.Log("Dragging " + gameObject.name);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        if (!isInSlot)
        {
            // Smoothly return the object to its original position
            StartCoroutine(SmoothMove(transform.position, parentAfterDrag.position, 0.8f, () =>
            {
                transform.SetParent(parentAfterDrag);
            }));
        }

        AudioManager.PlaySoundOneShot(SoundType.Drag);

        //Debug.Log("Stop Dragging " + gameObject.name);
    }

    public void SetText(string newText) 
    {
        text.text = newText;
    }

    public IEnumerator SmoothMove(Vector3 start, Vector3 end, float duration, System.Action onComplete = null)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end; // Snap to the final position
        onComplete?.Invoke();
    }
}
