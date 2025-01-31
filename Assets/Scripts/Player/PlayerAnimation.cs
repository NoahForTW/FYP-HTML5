using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    void Start()
    {

        animator = GetComponent<Animator>();
        PlayerController.Instance.currentPlayerActionEvent.AddListener(SetAnimation);
    }

    private void FixedUpdate()
    {
        animator.SetBool("Jump", PlayerController.Instance.notGrounded);
    }

    void SetAnimation(PlayerAction action)
    {
        animator.SetBool("Idle", action == global::PlayerAction.Idle && !PlayerController.Instance.notGrounded);
        animator.SetBool("Run", (action == global::PlayerAction.Right || action == global::PlayerAction.Left) 
            && !PlayerController.Instance.notGrounded); // if player is not currently jumping
    }

   
}
