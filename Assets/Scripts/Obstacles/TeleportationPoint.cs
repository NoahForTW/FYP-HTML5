using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeleportPlayerHandler))]
public class TeleportationPoint : MonoBehaviour
{
    bool teleported = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!teleported)
        {
            GetComponent<TeleportPlayerHandler>().TeleportPlayer();
            teleported = true;
        }
    }
}
