using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinInventory : MonoBehaviour
{
    public int NoOfCoins{get; private set;}

    public UnityEvent<CoinInventory> OnCoinCollected;

    public void CoinCollected()
    {
        NoOfCoins++;
        Debug.Log(NoOfCoins);
        OnCoinCollected.Invoke(this);
    }

}
