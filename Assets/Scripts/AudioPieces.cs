using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AudioPieces : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private TMP_Text text;
    private CanvasGroup canvasGroup;

    [HideInInspector] public Transform parentAfterDrag;

    public bool isInSlot = false;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        parentAfterDrag = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;

        AudioManager.PlaySoundOneShot(SoundType.Drag);
        //Debug.Log("Picked " + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

        AudioManager.PlaySoundOneShot(SoundType.Drag);
        //Debug.Log("Dragging " + gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isInSlot)
        {
            // Smoothly return the object to its original position
            StartCoroutine(SmoothMove(transform.position, parentAfterDrag.position, 0.8f, () =>
            {
                transform.SetParent(parentAfterDrag);
            }));
        }

        AudioManager.PlaySoundOneShot(SoundType.Drag);

        canvasGroup.blocksRaycasts = true;
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
