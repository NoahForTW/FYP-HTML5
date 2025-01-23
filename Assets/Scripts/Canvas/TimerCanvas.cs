using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.UIElements;
using UnityEngine.Playables;

public class TimerCanvas : MonoBehaviour
{
    [SerializeField] public TMP_Text timerText;
    [SerializeField] public GameObject notification;
    [SerializeField] public TextMeshProUGUI notificationText;
    [SerializeField] public GameObject notificationCloseMinigameButton;
    [SerializeField] public GameObject notificationCloseWindowButton;
    [SerializeField] public GameObject notificationPauseButtons;

    public void SetPauseNotif()
    {
        notification.SetActive(true);
        notificationText.text = "Are you sure to quit this game?\r\n<size=75%><color=red>Your progress would be lost</color></size>";
        notificationCloseMinigameButton.SetActive(false);
        notificationPauseButtons.SetActive(true);
    }

    public void SetGameDoneNotif(string remaindingTime , float coinsEarn, float score)
    {
        notification.SetActive(true);
        notificationText.text =
            @$"<u>Results</u>
<line-height=75%>
<size=70%>Score:</size>
<color=red>{score}</color><line-height=60%>
<align=center>----------------
<size=70%><align=left>Time Left:<line-height=0>
<align=right><color=yellow>{remaindingTime}</color><line-height=1em>
<align=left>Coins Earned:<line-height=0>
<align=right><color=yellow>{coinsEarn}</color><line-height=1em></size>";
        notificationCloseMinigameButton.SetActive(true);
        notificationPauseButtons.SetActive(false);
    }

}
