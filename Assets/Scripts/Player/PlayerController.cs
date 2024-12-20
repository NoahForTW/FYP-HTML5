using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerAction
{
    Jump,
    Left,
    Right,
    Interact,
    Idle
}

public class PlayerController : MonoBehaviour
{
    //public
    public static PlayerController Instance;
    public UnityEvent<PlayerAction> playerAction;
    public bool canMove = true;

    //private
    [SerializeField] private float movementSpeed = 1f;

    [SerializeField] private float jumpForce = 1f;

    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private GameObject itemPrefab;

    private Rigidbody playerRb;
    private Vector3 direction = new Vector3();

    public bool isJumping = false; // check if player is jumping
    public UnityEvent<PlayerAction> currentPlayerAction;

    private float playerHeight;
    private float lastActionTime = 0f; // Tracks the time of the last action
    private float inactivityThreshold = 0.5f;
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

        playerRb = GetComponent<Rigidbody>();
        //playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerAction.AddListener(PlayerAction);
        SetCurrentPlayerAction(global::PlayerAction.Idle);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time - lastActionTime > inactivityThreshold && !isJumping)
        {
            SetCurrentPlayerAction(global::PlayerAction.Idle);
        }

        Debug.Log("current action: " + currentPlayerAction.ToString());
        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = Mathf.Abs(vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //Debug.DrawLine(transform.position, worldPos, Color.red);
        //Debug.Log(worldPos);
    }

    public void SetCurrentPlayerAction(PlayerAction action)
    {
        currentPlayerAction.Invoke(action);
    }
    public void PlayerAction(PlayerAction action)
    {
        lastActionTime = Time.time;
        if (!canMove) { return; }
        switch (action)
        {
            case global::PlayerAction.Jump:
                PlayerJump();
                break;


            case global::PlayerAction.Left:
            case global::PlayerAction.Right:
                PlayerMovement(action);
                break;

        }
        SetCurrentPlayerAction(action);
        //playerAnimator.SetBool("Idle", action == global::PlayerAction.Idle);
        //playerAnimator.SetBool("Walk", action == global::PlayerAction.Right || action == global::PlayerAction.Left);

    }


    private void PlayerMovement(PlayerAction action)
    {
        direction = action == global::PlayerAction.Right ? transform.right : -transform.right;
        float currentForce = isJumping ? Mathf.Abs(movementSpeed - jumpForce) : movementSpeed;
        //playerRb.AddForce(direction * currentForce);
        playerRb.velocity = direction * currentForce;
    }

    private void PlayerJump()
    {
        if (!isJumping)
        {
            direction = transform.up;
            playerRb.AddForce(direction * jumpForce, ForceMode.Impulse);
            AudioManager.PlaySoundOneShot(SoundType.Jumping);
            isJumping = true;
        }
    }


    /*    private void OnDrawGizmos()
        {
            Vector3 mousePositionDirection = Input.mousePosition - transform.position;
            mousePositionDirection.z = 0;
            Gizmos.color = Color.blue;
            //Gizmos.DrawLine(transform.position, transform.position + mousePositionDirection.normalized * 100);
            Gizmos.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }*/

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}