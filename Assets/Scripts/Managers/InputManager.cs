using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [SerializeField] KeyCode jumpKey;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode interactingKey;


    public static InputManager Instance;


    private Dictionary<PlayerAction, Func<bool>> keyActions = new Dictionary<PlayerAction, Func<bool>>();
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {

            Instance = this;
        }

        

        keyActions = new Dictionary<PlayerAction, Func<bool>>
            {
                { PlayerAction.Jump, () => Input.GetKeyDown(jumpKey) },
                { PlayerAction.Left, () => Input.GetKey(leftKey) },
                { PlayerAction.Right, () => Input.GetKey(rightKey) },
                { PlayerAction.Interact, () => Input.GetKeyDown(interactingKey) },

            };
    }

    void Update()
    {
        foreach (var action in keyActions)
        { 

            if (action.Value())
            {
                PlayerController.Instance.playerAction.Invoke(action.Key);

            }
        }

    }

    public void SetKey(KeyCode newKeycode)
    {

    }

}
