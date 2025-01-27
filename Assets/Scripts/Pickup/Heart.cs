using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PickUpInventory _pkInventory = other.GetComponent<PickUpInventory>();

        if (_pkInventory != null)
        {
            _pkInventory.HeartCollected();
            Destroy(gameObject);

            AudioManager.instance.PlaySoundOneShot(SoundType.PickUpPotion);
        }
    }
}