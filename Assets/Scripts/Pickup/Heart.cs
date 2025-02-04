using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInventory.Instance.HeartCollected.Invoke();
            this.gameObject.SetActive(false);
            AudioManager.instance.PlaySoundOneShot(SoundType.PickUpPotion);
        }
    }
}