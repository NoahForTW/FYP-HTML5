using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSFX : MonoBehaviour
{
    public void ObstacleFX()
    {
        AudioManager.instance.PlaySoundOneShot(SoundType.Lever);
    }
}
