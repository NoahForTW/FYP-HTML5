using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PickUpInventory _pkInventory = other.GetComponent<PickUpInventory>();

        if (_pkInventory != null)
        {
            _pkInventory.CoinCollected();
            gameObject.SetActive(false);

            AudioManager.instance.PlaySoundOneShot(SoundType.PickUpCoin);
        }
    }
}
