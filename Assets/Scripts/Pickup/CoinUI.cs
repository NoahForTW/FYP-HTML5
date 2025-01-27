using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TMP_Text _pkText;

    void Start()
    {
        _pkText = GetComponent<TMP_Text>();
    }

    public void UpdateCoinText(PickUpInventory _pkInventory)
    {
        _pkText.text = _pkInventory.NoOfCoins.ToString();
    }
}
