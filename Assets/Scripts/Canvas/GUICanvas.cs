using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUICanvas : MonoBehaviour
{
    [Header("Mobile Controls")]
    public GameObject MobileControlsUI;

    [Header("PC Controls")]
    public GameObject PCControlsUI;
    public GameObject JumpKeyText;
    public GameObject InteractKeyText;

    [Header("Coins UI")]
    public TextMeshProUGUI CoinsText;
    public CanvasGroup CanvasGroup;

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();

        // hide mobile controls & show PC GUI when device is PC/Windows (vice versa)
        MobileControlsUI.SetActive(Application.isMobilePlatform);
        PCControlsUI.SetActive(!Application.isMobilePlatform);
    }

    public void SetActiveControlsUI(bool isActive)
    {
        // set visibility based on device platform
        if (Application.isMobilePlatform)
            MobileControlsUI.SetActive(isActive);
        else
            PCControlsUI.SetActive(isActive);
    }


}
