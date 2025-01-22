using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    private TextMeshProUGUI _coinText;

    void Start()
    {
        _coinText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinText(CoinInventory _coinInventory)
    {
        _coinText.text = _coinInventory.NoOfCoins.ToString();
    }
}
