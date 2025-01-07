using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUICanvas : MonoBehaviour
{
    public GameObject PlayerControlsUI;
    public TextMeshProUGUI CoinsText;
    public CanvasGroup CanvasGroup;

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }
}
