using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public class Minigame : MonoBehaviour
{
    public MinigameType minigameType;
    public bool isCompleted = false;
    public float maxTimeInSeconds = 0f;
    [TextArea]
    public string currentClue;

    public GameObject Window;
    public TMP_Text ClueCostText;
    protected virtual void OnEnable()
    {
        RectTransform windownRect = Window?.GetComponent<RectTransform>();
        Vector3 currentPosition = windownRect.anchoredPosition3D;
        RectTransform startTransform = windownRect;
        startTransform.anchoredPosition3D = currentPosition - transform.up * 400;
        Tween.UIAnchoredPosition(startTransform, currentPosition, duration: 1, ease: Ease.OutCubic);

        // subscribe clue cost 
        MinigameManager.Instance.clueCostUpdated.AddListener(UpdateClueCost);
        UpdateClueCost();
    }
    private void OnDisable()
    {
        MinigameManager.Instance.clueCostUpdated.RemoveListener(UpdateClueCost);
    }
    public virtual void StartMinigame()
    {
        this.gameObject.SetActive(true);
        isCompleted = false;
        MinigameManager.Instance.SetTimer(maxTimeInSeconds);
    }

    public virtual void EndMinigame()
    {
        this.gameObject.SetActive(false);

        // * I think can add like a visual "Success" thingy panel here ??
        if (isCompleted) 
            AudioManager.instance.PlaySoundOneShot(SoundType.Successful);
    }

    public void ShowClue()
    {
        MinigameManager.Instance.ShowClue(currentClue);
    }

    void UpdateClueCost()
    {
        ClueCostText.text = MinigameManager.Instance.GetCurrentClueCost().ToString();
    }



}
