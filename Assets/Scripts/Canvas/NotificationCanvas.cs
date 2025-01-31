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
    public TextMeshProUGUI notificationText;
    public GameObject notificationCloseMinigameButton;
    public GameObject notificationCloseWindowButton;
    public GameObject notificationPauseButtons;

    UnityEvent<TypeOfNotif> showNotificationEvent;

    private void Awake()
    {
        if(showNotificationEvent == null)
            showNotificationEvent = new UnityEvent<TypeOfNotif>();
        showNotificationEvent.AddListener(showNotification);
    }
    public void SetPauseNotif()
    {
        notificationText.text = "Are you sure to quit this game?\r\n<size=75%><color=red>Your progress would be lost</color></size>";
        showNotificationEvent.Invoke(TypeOfNotif.Pause);
    }

    public void SetGameDoneNotif(string remaindingTime, float coinsEarn)
    {
        notificationText.text =
            @$"<u>Results</u>
<size=70%><align=left>Time Left:<line-height=0>
<align=right><color=yellow>{remaindingTime}</color><line-height=1em>

<align=left>Coins Earned:<line-height=0>
<align=right><color=yellow>{coinsEarn}</color><line-height=1em></size>";
        showNotificationEvent.Invoke(TypeOfNotif.GameDone);

    }

    public void SetNotif(string text)
    {
        notificationText.text = text;
        showNotificationEvent.Invoke(TypeOfNotif.Notification);
    }

    public void showNotification(TypeOfNotif type)
    {
        notification.SetActive(true);
        notificationCloseWindowButton.SetActive(type == TypeOfNotif.Notification);
        notificationCloseMinigameButton.SetActive(type == TypeOfNotif.GameDone);
        notificationPauseButtons.SetActive(type == TypeOfNotif.Pause);
    }
}
