using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInventory : MonoBehaviour
{
    public int NoOfCoins{get; private set;}

    public void CoinCollected()
    {
        NoOfCoins++;
    }
}
