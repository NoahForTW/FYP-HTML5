using Cinemachine;
using UnityEngine;

public enum PlayerAction {
    Jump,
    Crouch,
    Crouched,
    Sprinting,
    Sprinted,
    Left,
    Right,
    Aim
}

public class PlayerController : MonoBehaviour
{
    //public
    public static PlayerController Instance;
    public bool canMove = true;

    //private
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float sprintingSpeed = 1f;
    [SerializeField] float jumpForce = 1f;

    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] GameObject itemPrefab;
    Rigidbody playerRb;
    Vector3 direction = new Vector3();
    Vector3 facingDirection = new Vector3(1, 1, 1);

    bool isJumping = false;
    bool isSprinting = false;

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
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.DrawLine(transform.position, worldPos, Color.red);
        //Debug.Log(worldPos);
    }

    

    void PlayerAction(PlayerAction action)
    {
        
        direction = Vector3.zero;
        if (!canMove) { return; }
        //float currentForce = movementSpeed;
        //ForceMode forceMode = ForceMode.Force;
        //Debug.LogError(action);

        switch (action)
        {
            case global::PlayerAction.Sprinting:
                SetSprint(true);
                break;
            case global::PlayerAction.Sprinted:
                SetSprint(false);
                break;

            case global::PlayerAction.Jump:
                PlayerJump();
                break;

            case global::PlayerAction.Crouch:
                PlayerCrouch();
                break;

            case global::PlayerAction.Crouched:
                PlayerCrouched();
                break;

            case global::PlayerAction.Left:
            case global::PlayerAction.Right:
                PlayerMovement(action);
                break;

            case global::PlayerAction.Aim:
                // do shit
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                SpawnGO(worldPos);
                break;
        }

        //playerRb.AddForce(direction.normalized * currentForce, forceMode);

        //transform.position = new Vector3(transform.position.x + movementSpeed, transform.position.y, transform.position.z);
        
    }

    void SetSprint(bool isSprint)
    {
        isSprinting = isSprint;
    }
    void PlayerMovement(PlayerAction action)
    {
        direction = action == global::PlayerAction.Right ? transform.right : -transform.right;
        facingDirection.x = action == global::PlayerAction.Right ? 1 : -1;
        float currentSpeed = isSprinting ? sprintingSpeed : movementSpeed;
        float currentForce = isJumping ? Mathf.Abs(currentSpeed - jumpForce) : currentSpeed;
        transform.localScale = facingDirection;
        playerRb.AddForce(direction * currentForce);
        playerRb.maxLinearVelocity = currentForce;
        //playerRb.velocity = direction * currentForce;
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

    /*    private void OnDrawGizmos()
        {
            Vector3 mousePositionDirection = Input.mousePosition - transform.position;
            mousePositionDirection.z = 0;
            Gizmos.color = Color.blue;
            //Gizmos.DrawLine(transform.position, transform.position + mousePositionDirection.normalized * 100);
            Gizmos.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }*/

    void SpawnGO(Vector3 position)
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

}
