using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinUI : MonoBehaviour
{
    [SerializeField] TMP_Text _pkText;

    private void Start()
    {
        PlayerInventory.Instance.UpdateCoinUI.AddListener(UpdateCoinText);
    }

    public void UpdateCoinText()
    {
        _pkText.text = PlayerInventory.Instance.GetCurrentCoins().ToString();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateCoinText();
    }

}
