using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] KeyCode jumpKey01;
    [SerializeField] KeyCode jumpKey02;
    [SerializeField] KeyCode crouchKey;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode sprintingKey;
    [SerializeField] KeyCode interactKey;


    public static InputManager Instance;

    public UnityEvent<PlayerAction> playerAction;

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
            { PlayerAction.Aim, () => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() },
            { PlayerAction.Interact, () => Input.GetKeyDown(interactKey) }


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
                playerAction.Invoke(action.Key); 
                noKeyPress = false;
            }
        }

/*        if (noKeyPress 
            && !PlayerController.Instance.isJumping
            && !PlayerController.Instance.isCrouching)
        {
            playerAction.Invoke(PlayerAction.Idle);
        }*/
        // gravity chnage (for mobile)
/*        if (Input.GetKeyDown(KeyCode.V))
        {
            Physics.gravity = new Vector3(-9.81f, 0, 0);
        }*/
    }

}
