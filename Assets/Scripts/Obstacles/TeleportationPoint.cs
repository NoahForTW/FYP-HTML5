using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeleportPlayerHandler))]
public class TeleportationPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<TeleportPlayerHandler>().TeleportPlayer();
    }
}
