using Cinemachine;
using System.Collections;
using Unity.VisualScripting;
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
    [HideInInspector] public UnityEvent<PlayerAction> playerAction;
    public bool canMove = true;
    public bool isJumping = false; // check if player is jumping
    public UnityEvent<PlayerAction> currentPlayerActionEvent;
    public PlayerAction currentPlayerAction;
    //private

    [Header ("Speeds")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;

    private Rigidbody playerRb;
    private GameObject playerModel;
    private Vector3 direction = new Vector3();

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
        playerModel = transform.GetChild(0).gameObject;
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
            playerRb.velocity = Vector3.zero;
        }

    }

    public void SetCurrentPlayerAction(PlayerAction action)
    {
        currentPlayerActionEvent.Invoke(action);
        currentPlayerAction = action;
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
        playerRb.AddForce(direction * movementSpeed);
        //playerRb.velocity = direction * movementSpeed;
        float yRotation = action == global::PlayerAction.Left ? 180f : 0f;
        Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
        //StartCoroutine(RotateModel(rotation, 0.3f));
        playerModel.transform.rotation = rotation;
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

    IEnumerator RotateModel(Quaternion rotateTo, float duration)
    {
        float elapsed = 0f;

        Quaternion currentRotation = playerModel.transform.rotation;

        while (elapsed < duration)
        {
            playerModel.transform.rotation = Quaternion.Lerp(currentRotation, rotateTo, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
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