using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    private TextMeshProUGUI _pkText;

    void Start()
    {
        _pkText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinText(PickUpInventory _pkInventory)
    {
        _pkText.text = _pkInventory.NoOfCoins.ToString();
    }
}
