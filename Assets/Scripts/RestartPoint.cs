using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPoint : MonoBehaviour
{
    [SerializeField] private Transform startPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EndPoint"))
        {
            TeleportToStart();

            Debug.Log("Hit Endpoint");
        }
        if(other.CompareTag("Void"))
        {
            TeleportToStart();

            Debug.Log("Hit Void");
        }
    }

    private void TeleportToStart()
    {
        transform.position = startPoint.position;

        Debug.Log("Teleported back to start");
    }
}
