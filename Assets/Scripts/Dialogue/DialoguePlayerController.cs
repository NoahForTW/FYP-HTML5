using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float moveSpeed;
    private Vector3 moveDirection;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            return;
        }
        
        moveDirection.x = Input.GetAxis("Horizontal") * moveSpeed;
        moveDirection.z = Input.GetAxis("Vertical") * moveSpeed;
        moveDirection.y = playerRb.velocity.y;
        playerRb.velocity = moveDirection;
    }
}
