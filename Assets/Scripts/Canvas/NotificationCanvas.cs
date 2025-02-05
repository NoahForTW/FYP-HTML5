using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum TypeOfNotif
{
    Pause,
    Notification,
    GameDone
}

public class NotificationCanvas : MonoBehaviour
{
    public GameObject notification;
    public GameObject notificationTextGO;
    [SerializeField] TextMeshProUGUI notificationText;
    [SerializeField] TextMeshProUGUI notificationWindowText;
    [SerializeField] GameObject notificationCloseMinigameButton;
    [SerializeField] GameObject notificationCloseWindowButton;
    [SerializeField] GameObject notificationPauseButtons;

    UnityEvent<TypeOfNotif> showNotificationEvent;
    Coroutine showNotificationText;
    bool ShowTextDone = true;

    private void Awake()
    {
        if(showNotificationEvent == null)
            showNotificationEvent = new UnityEvent<TypeOfNotif>();
        showNotificationEvent.AddListener(ShowNotification);
    }
    public void SetPauseNotif()
    {
        notificationWindowText.text = "Are you sure to quit this game?\r\n<size=75%><color=red>Your progress would be lost</color></size>";
        showNotificationEvent.Invoke(TypeOfNotif.Pause);
    }

    public void SetGameDoneNotif(string remaindingTime, float coinsEarn)
    {
        notificationWindowText.text =
            @$"<u>Results</u>
<size=70%><align=left>Time Left:<line-height=0>
<align=right><color=yellow>{remaindingTime}</color><line-height=1em>

<align=left>Coins Earned:<line-height=0>
<align=right><color=yellow>{coinsEarn}</color><line-height=1em></size>";
        showNotificationEvent.Invoke(TypeOfNotif.GameDone);

    }

    public void SetWindowNotif(string text)
    {
        notificationWindowText.text = text;
        showNotificationEvent.Invoke(TypeOfNotif.Notification);
    }

    public void ShowNotification(TypeOfNotif type)
    {
        notification.SetActive(true);
        notificationCloseWindowButton.SetActive(type == TypeOfNotif.Notification);
        notificationCloseMinigameButton.SetActive(type == TypeOfNotif.GameDone);
        notificationPauseButtons.SetActive(type == TypeOfNotif.Pause);
    }

    public void StartTextNotif(string text)
    {
        if (!ShowTextDone)
            return;

        if (showNotificationText != null)
            StopCoroutine(showNotificationText);
        showNotificationText = StartCoroutine(ShowTextNotification(text));
    }
    IEnumerator ShowTextNotification(string text)
    {
        ShowTextDone = false;
        notificationText.text = text;
        notificationTextGO.SetActive(true);

        RectTransform windownRect = notificationTextGO?.GetComponent<RectTransform>();
        Vector3 currentPosition = windownRect.anchoredPosition3D;
        RectTransform startTransform = windownRect;
        startTransform.anchoredPosition3D = currentPosition - transform.up * 100;
        Tween.UIAnchoredPosition(startTransform, currentPosition, duration: 1.5f, ease: Ease.OutCubic);

        yield return new WaitForSeconds(1.5f);
        notificationTextGO.SetActive(false);
        ShowTextDone = true;
    }
}
