using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [SerializeField] KeyCode jumpKey;
    [SerializeField] KeyCode crouchKey;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;

    public static InputManager Instance;

    public UnityEvent<string> movePlayer;

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


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            movePlayer.Invoke("jump");
        }
        else if (Input.GetKeyDown(crouchKey))
        {
            movePlayer.Invoke("crouch");
        }
        else if (Input.GetKeyUp(crouchKey))
        {
            movePlayer.Invoke("crouched");
        }
        else if (Input.GetKey(leftKey))
        {
            movePlayer.Invoke("left");
        }
        else if (Input.GetKey(rightKey))
        {
            movePlayer.Invoke("right");
        }
    }

}
