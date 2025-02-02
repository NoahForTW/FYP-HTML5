using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInventory.Instance.CoinCollected.Invoke();
            this.gameObject.SetActive(false);
            AudioManager.instance.PlaySoundOneShot(SoundType.PickUpCoin);
        }

    }
}
