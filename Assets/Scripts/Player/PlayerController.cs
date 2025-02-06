using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum PlayerAction
{
    Jump,
    Left,
    Right,
    Interact,
    Idle, 
    Pause,
    Stun,
    Die,
    None
}

public class PlayerController : MonoBehaviour
{
    //public
    public static PlayerController Instance;
    [HideInInspector] public UnityEvent<PlayerAction> playerAction;
    public bool canMove = true;
    public bool stunned = false;
    public bool notGrounded = false; // check if player is jumping
    public UnityEvent<PlayerAction> currentPlayerActionEvent;
    public PlayerAction currentPlayerAction;
    public PlayerAction MovingDirection;
    public bool Jumping;
    float stunTime;
    [SerializeField] float stunDuration;
    private float lastSoundTime = 0f; // Tracks the last time a walking sound was played
    [SerializeField] private float walkingSoundCooldown = 0.3f; // Cooldown in seconds for walking sound
    //private

    [Header ("Speeds")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;

    private Rigidbody playerRb;
    private GameObject playerModel;
    private Vector3 direction = new Vector3();

    private float lastActionTime = 0f; // Tracks the time of the last action
    private float inactivityThreshold = 0.05f;
    private void Awake()
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
        SetCurrentPlayerAction(global::PlayerAction.Jump);

 
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
        canMove = !DialogueManager.GetInstance().dialogueIsPlaying && MinigameManager.Instance.GetCurrentMinigame() == null && !stunned;
        if (stunned)
        {
            stunTime += Time.deltaTime;
            if (stunTime >= stunDuration)
            {
                stunTime = 0;
                stunned = false;
            }
            if (MovingDirection == global::PlayerAction.None)
                return;
            SetCurrentPlayerAction(global::PlayerAction.Stun);
        }

        if (notGrounded)
        {
            SetCurrentPlayerAction(global::PlayerAction.Jump);
        }
        else
        {
            if (MovingDirection == global::PlayerAction.None)
                PlayerAction(global::PlayerAction.Idle);

        }

            

    }
    private void FixedUpdate()
    {
        playerRb.velocity = new Vector3(0,playerRb.velocity.y, playerRb.velocity.z);
        if (!canMove) { return; }

        if (MovingDirection == global::PlayerAction.Left || MovingDirection == global::PlayerAction.Right)
        { 
            PlayerMovement(MovingDirection);
            MovingDirection = global::PlayerAction.None;
        }

        // Process jump action
        if (Jumping)
        {
            PlayerJump();

        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string RespawnTag = null;
        if (SceneManager.GetActiveScene().name == "GDTLevel")
        {
            RespawnTag = SavePlayerData.Instance.LoadData<GDTLevelData>().RespawnTag;
        }
        else if (SceneManager.GetActiveScene().name == "AGVEScene")
        {
            RespawnTag = SavePlayerData.Instance.LoadData<AGVELevelData>().RespawnTag;
        }
        if (!string.IsNullOrEmpty(RespawnTag))
        {
            SpawnPlayer(RespawnTag);
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
        if (action == global::PlayerAction.Stun || stunned)
        {
            stunned = true;
            return;
        }
        if (action == global::PlayerAction.Left || action == global::PlayerAction.Right)
        {
            MovingDirection = action;
        }
        else if (action == global::PlayerAction.Jump)
        {
            Jumping = true;
        }

        SetCurrentPlayerAction(action);
        
    }


    private void PlayerMovement(PlayerAction action)
    {
        // player movement
       direction = action == global::PlayerAction.Right ? transform.right : -transform.right;
        /*  float currentForce = isJumping ? Mathf.Abs(movementSpeed - jumpForce) : movementSpeed;
         playerRb.AddForce(direction * movementSpeed);*/
        Vector3 movement = direction * movementSpeed;
        playerRb.velocity = new Vector3(movement.x, playerRb.velocity.y, 0);

        // rotating player 
        float yRotation = action == global::PlayerAction.Left ? 180f : 0f;
        Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
        //StartCoroutine(RotateModel(rotation, 0.3f));
        playerModel.transform.rotation = rotation;

        // Play walking sound if cooldown has passed
        if (!notGrounded && Time.time - lastSoundTime > walkingSoundCooldown)
        {
            AudioManager.instance.PlaySoundOneShot(SoundType.Walking, 0.7f);
            lastSoundTime = Time.time; // Update the last sound time
        }

        Debug.LogError("Movement " + movement);
    }

    private void PlayerJump()
    {
        if (!notGrounded)
        {
            direction = transform.up;
            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, 0);
            //playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            AudioManager.instance.PlaySoundOneShot(SoundType.Jumping);
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
    
    public void SpawnPlayer(string tagName)
    {
        GameObject PlayerSpawnPoint = null;
        Scene s = SceneManager.GetActiveScene();
        GameObject[] rootGameObjects = s.GetRootGameObjects();
        foreach (GameObject go in rootGameObjects)
        {
            if (go.CompareTag(tagName))
            {
                PlayerSpawnPoint = go;
                break;
            }
        }

        if (PlayerSpawnPoint == null)
            return;

        transform.position = PlayerSpawnPoint.transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            notGrounded = false;
            Jumping = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            notGrounded = true;

        }
    }
    
}