using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float jumpForce = 1f;

    Rigidbody playerRb;
    Vector3 direction = new Vector3();

    bool isJumping = false;
    bool isGroubnded = false;
    float playerHeight;
    void Start()
    {
        //inputManager = InputManager.Instance;
        InputManager.Instance.movePlayer.AddListener(PlayerAction);
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       

       
    }

    void PlayerAction(string action)
    {
        direction = Vector3.zero;
        float currentForce = movementSpeed;
        ForceMode forceMode = ForceMode.Force;
        switch (action)
        {
            case "jump":
                PlayerJump();
                break;

            case "crouch":
                PlayerCrouch();
                break;

            case "crouched":
                PlayerCrouched();
                break;

            case "left":
                direction = -transform.right;
                break;

            case "right":
                direction = transform.right;
                break;
        }

        playerRb.AddForce(direction.normalized * currentForce, forceMode);

        //transform.position = new Vector3(transform.position.x + movementSpeed, transform.position.y, transform.position.z);
        
    }

    void PlayerJump()
    {
        if (!isJumping)
        {
            direction = transform.up;
            playerRb.AddForce(direction * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    void PlayerCrouch()
    {
        // when crouch button is hold

        playerHeight = transform.localScale.y;

        transform.localScale = new Vector3(transform.localScale.x, playerHeight / 2, transform.localScale.z);
    }
    void PlayerCrouched()
    {
        // when player lets go of crouch key - basically uncrouch   
        transform.localScale = new Vector3(transform.localScale.x, playerHeight, transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + direction * 100);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            isJumping = false;
        }
    }

}
