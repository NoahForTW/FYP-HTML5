using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [SerializeField] KeyCode jumpKey01;
    [SerializeField] KeyCode jumpKey02;
    [SerializeField] KeyCode crouchKey;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode sprintingKey;


    public static InputManager Instance;

    public UnityEvent<PlayerAction> movePlayer;

    private Dictionary<PlayerAction,Func<bool>> keyActions = new Dictionary<PlayerAction,Func<bool>>();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else {

            Instance = this;
        }

        keyActions = new Dictionary<PlayerAction, Func<bool>>
        {
            { PlayerAction.Jump, () => Input.GetKeyDown(jumpKey01) || Input.GetKeyDown(jumpKey02) },
            { PlayerAction.Crouch, () => Input.GetKeyDown(crouchKey) },
            { PlayerAction.Crouched, () => Input.GetKeyUp(crouchKey) },
            { PlayerAction.Sprinting, () => Input.GetKey(sprintingKey) },
            { PlayerAction.Sprinted, () => Input.GetKeyUp(sprintingKey) },
            { PlayerAction.Left, () => Input.GetKey(leftKey) },
            { PlayerAction.Right, () => Input.GetKey(rightKey) },
            { PlayerAction.Aim, () => Input.GetMouseButtonDown(0) }

        };
    }

    // Update is called once per frame
    void Update()
    {
        bool noKeyPress = true;
        foreach (var action in keyActions)
        {
            if (action.Value())
            {
                movePlayer.Invoke(action.Key); 
                noKeyPress = false;
            }
        }

        if (noKeyPress 
            && !PlayerController.Instance.isJumping
            && !PlayerController.Instance.isCrouching)
        {
            movePlayer.Invoke(PlayerAction.Idle);
        }
    }

}
