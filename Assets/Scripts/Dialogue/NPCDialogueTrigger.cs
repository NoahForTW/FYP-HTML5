using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    void Awake()
    {
        playerInRange = false;
    }

    void Update()
    {
        if (playerInRange) {
            if (Input.GetKey(KeyCode.Space)) {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            playerInRange = false;
        }
    }
}
