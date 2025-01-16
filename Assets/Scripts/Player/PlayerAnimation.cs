using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerController controller;
    Animator animator;
    void Start()
    {
        if (PlayerController.Instance != null)
            controller = PlayerController.Instance;

        animator = GetComponent<Animator>();
        controller?.currentPlayerActionEvent.AddListener(SetAnimation);
    }

    private void FixedUpdate()
    {
        animator.SetBool("Jump", controller.notGrounded);
    }

    void SetAnimation(PlayerAction action)
    {
        animator.SetBool("Idle", action == global::PlayerAction.Idle);
        animator.SetBool("Run", (action == global::PlayerAction.Right || action == global::PlayerAction.Left) 
            && !controller.notGrounded); // if player is not currently jumping
    }
}
