using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnModel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerFeet"))
        {
            other.transform.parent.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerFeet"))
        {
            other.transform.parent.SetParent(null);
        }
    }
}
