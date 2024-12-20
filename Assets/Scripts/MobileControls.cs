using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum MobilePlayerControls
{
    Jump,
    Crouch,
    Left,
    Right
}

enum MovementDirection
{
    None,
    Left,
    Right
}

public class MobileControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MobilePlayerControls mobilePlayerControls;

    MovementDirection movementDirection = MovementDirection.None;

    private void Update()
    {
        if(movementDirection == MovementDirection.Left)
        {
            PlayerController.Instance.playerAction.Invoke(PlayerAction.Left);
        }
        if(movementDirection == MovementDirection.Right)
        {
            PlayerController.Instance.playerAction.Invoke(PlayerAction.Right);

        }
    }

    // Mobile controls

    public void DoAction()
    {
        switch (mobilePlayerControls)
        {
            case MobilePlayerControls.Jump:
            break;
        }
    }
    public void UpButton()
    {
        PlayerController.Instance.playerAction.Invoke(PlayerAction.Jump);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (mobilePlayerControls)
        {
            case MobilePlayerControls.Jump:
                PlayerController.Instance.playerAction.Invoke(PlayerAction.Jump);
                break;
            case MobilePlayerControls.Left:
                movementDirection = MovementDirection.Left;
                break;
            case MobilePlayerControls.Right:
                movementDirection = MovementDirection.Right;
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        movementDirection = MovementDirection.None;
    }
}
