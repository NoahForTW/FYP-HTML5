using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpInventory : MonoBehaviour
{
    public int NoOfCoins { get; private set; }
    public int NoOfHearts { get; private set; }

    public UnityEvent<PickUpInventory> OnCoinCollected;
    public UnityEvent<PickUpInventory> OnHeartCollected;

    public void CoinCollected()
    {
        NoOfCoins++;
        Debug.Log(NoOfCoins);
        OnCoinCollected.Invoke(this);
    }

    public void HeartCollected()
    {
        //Put ur heart gain logic here
        // if uw use the int at the top also can
        //glhf
    }

    public void CosmeticCollected()
    {

    }
}
