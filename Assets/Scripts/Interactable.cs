using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] UnityEvent interactionEvent;

  
    bool canInteract=false;
    void Start()
    {
        PlayerController.Instance.playerAction.AddListener(PlayerAction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract =false;
        }
    }

    void PlayerAction(PlayerAction action)
    {
        if (action == global::PlayerAction.Interact && canInteract)
        {
            interactionEvent.Invoke();
        }
    }
}
