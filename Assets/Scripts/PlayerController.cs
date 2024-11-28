using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum PlayerAction
{
    Jump,
    Crouch,
    Crouched,
    Sprinting,
    Sprinted,
    Left,
    Right,
    Aim,
    Idle,
    Interact
}

public class PlayerController : MonoBehaviour
{
    //public
    public static PlayerController Instance;
    public UnityEvent<PlayerAction> playerAction;
    public bool canMove = true;

    //private
    [SerializeField] private float movementSpeed = 1f;

    [SerializeField] private float sprintingSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;

    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private GameObject itemPrefab;

    private Rigidbody playerRb;
    private Vector3 direction = new Vector3();

    public bool isJumping = false; // check if player is jumping
    public bool isCrouching = false; // check if player is crouching
    private bool isSprinting = false;

    private float playerHeight;

    private void Awake  ()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        //inputManager = InputManager.Instance;

        playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerAction.AddListener(PlayerAction);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.DrawLine(transform.position, worldPos, Color.red);
        //Debug.Log(worldPos);
    }

    public void PlayerAction(PlayerAction action)
    {
        direction = Vector3.zero;
        if (!canMove) { return; }
        //float currentForce = movementSpeed;
        //ForceMode forceMode = ForceMode.Force;
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

        Debug.Log("is Idle:" + (action == global::PlayerAction.Idle));
    }

    private void SetSprint(bool isSprint)
    {
        isSprinting = isSprint;
    }

    private void PlayerMovement(PlayerAction action)
    {
        direction = action == global::PlayerAction.Right ? transform.right : -transform.right;
        float currentSpeed = isSprinting ? sprintingSpeed : movementSpeed;
        float currentForce = isJumping ? Mathf.Abs(currentSpeed - jumpForce) : currentSpeed;
        playerRb.AddForce(direction * currentForce);
        playerRb.maxLinearVelocity = currentForce;
        //playerRb.velocity = direction * currentForce;
    }

    private void PlayerJump()
    {
        if (!isJumping)
        {
            direction = transform.up;
            playerRb.AddForce(direction * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void PlayerCrouch()
    {
        // when crouch button is hold
        isCrouching = true;
        playerHeight = transform.localScale.y;

        transform.localScale = new Vector3(transform.localScale.x, playerHeight / 2, transform.localScale.z);
    }

    private void PlayerCrouched()
    {
        // when player lets go of crouch key - basically uncrouch
        transform.localScale = new Vector3(transform.localScale.x, playerHeight, transform.localScale.z);
        isCrouching = false;
    }

    /*    private void OnDrawGizmos()
        {
            Vector3 mousePositionDirection = Input.mousePosition - transform.position;
            mousePositionDirection.z = 0;
            Gizmos.color = Color.blue;
            //Gizmos.DrawLine(transform.position, transform.position + mousePositionDirection.normalized * 100);
            Gizmos.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }*/

    private void SpawnGO(Vector3 position)
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