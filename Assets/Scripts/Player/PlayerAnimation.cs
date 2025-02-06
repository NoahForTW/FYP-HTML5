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
        if (!PlayerController.Instance.canMove)
            return;
        animator.SetBool("Idle", action == global::PlayerAction.Idle && !PlayerController.Instance.notGrounded);
        animator.SetBool("Run", (action == global::PlayerAction.Right || action == global::PlayerAction.Left) 
            && !PlayerController.Instance.notGrounded); // if player is not currently jumping
        animator.SetBool("Stun", action == PlayerAction.Stun);

        if (action == PlayerAction.Die) 
            animator.Play("Die");


    }

   
}
