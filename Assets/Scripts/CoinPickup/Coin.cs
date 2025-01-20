using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CoinInventory _coinInventory = other.GetComponent<CoinInventory>();

        if (_coinInventory != null)
        {
            _coinInventory.CoinCollected();
            gameObject.SetActive(false);
        }
    }
}
