using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interacable : MonoBehaviour
{
    [SerializeField] GameObject interactButton;
    [SerializeField] UnityEvent interactionEvent;

    GameObject currentInteractionButton;
  
    bool canInteract=false;
    void Start()
    {
        PlayerController.Instance.playerAction.AddListener(PlayerAction);
        interactionEvent.AddListener(Interacting);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            // show Interaction 
            currentInteractionButton = Instantiate(interactButton, canvas.transform);
            canInteract = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (currentInteractionButton != null)
            {
                Destroy(currentInteractionButton);
                currentInteractionButton = null;
            }
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
    void Interacting()
    {
        Debug.Log("interacting");
    }
}
