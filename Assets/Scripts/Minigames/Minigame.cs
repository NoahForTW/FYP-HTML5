using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
public class Minigame : MonoBehaviour
{
    public MinigameType minigameType;
    public bool isCompleted = false;

    public GameObject Window;
    protected virtual void OnEnable()
    {
        RectTransform windownRect = Window?.GetComponent<RectTransform>();
        Vector3 currentPosition = windownRect.position;
        RectTransform startTransform = windownRect;
        startTransform.position = currentPosition - transform.up * 5;
        Tween.UIAnchoredPosition(startTransform, currentPosition, duration: 1, ease: Ease.OutCubic);
    }
    public virtual void StartMinigame()
    {
        this.gameObject.SetActive(true);
        isCompleted = false;
    }

    public virtual void EndMinigame()
    {
        this.gameObject.SetActive(false);
    }
}
