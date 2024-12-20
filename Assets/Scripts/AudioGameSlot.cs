using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioGameSlot : DropSlot
{
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        AudioManager.PlaySoundOneShot(SoundType.Drag);
    }
}
