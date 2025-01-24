using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType
    {
        GroundEnemy,  // Enemy that only moves between waypoints
        FlyEnemy      // Enemy that moves and can attack the player
    }

    public enum State
    {
        Idle,         // Enemy is idle
        Walking,      // Enemy is walking between waypoints
        Attacking,    // Enemy is attacking the player
        Dying         // Enemy is dying (placeholder for future use)
    }

    public EnemyType enemyType; // Set the type of the enemy in the Inspector
    public State currentState = State.Idle; // Enemy's current state
    public float idleDuration = 2.0f; // Time spent idle at each waypoint
    public float detectionRange = 5.0f; // Range for detecting the player
    public float attackRange = 2.0f; // Range for attacking the player
    public float attackCooldown = 1.5f; // Cooldown between attacks
    private float attackTimer = 0f; // Tracks attack cooldown

    public List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f; // Distance to waypoint
    private int lastWaypointIndex;

    private float movementSpeed = 5.0f;
    private Animator animator; // Reference to Animator
    private bool isIdleCoroutineRunning = false; // Prevent multiple coroutines

    public Transform player; // Reference to the player

    // Start is called before the first frame update
    void Start()
    {
        lastWaypointIndex = waypoints.Count - 1;
        targetWaypoint = waypoints[targetWaypointIndex]; // Set the first waypoint
        animator = GetComponent<Animator>(); // Get the Animator component
        ChangeState(State.Walking); // Start in the Walking state
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime; // Reduce attack cooldown timer

        switch (currentState)
        {
            case State.Idle:
                HandleIdleState();
                break;
            case State.Walking:
                HandleWalkingState();
                break;
            case State.Attacking:
                if (enemyType == EnemyType.FlyEnemy)
                    HandleAttackingState();
                break;
            case State.Dying:
                HandleDyingState(); // Placeholder for future implementation
                break;
        }
    }

    void ChangeState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.Idle:
                animator.SetBool("Walking", false);
                break;
            case State.Walking:
                animator.SetBool("Walking", true);
                break;
            case State.Attacking:
                animator.SetTrigger("Attack");
                break;
        }
    }

    void HandleIdleState()
    {
        if (!isIdleCoroutineRunning)
        {
            StartCoroutine(IdleDelay());
        }
    }

    IEnumerator IdleDelay()
    {
        isIdleCoroutineRunning = true;
        yield return new WaitForSeconds(idleDuration);
        ChangeState(State.Walking);
        isIdleCoroutineRunning = false;
    }

    void HandleWalkingState()
    {
        float movementStep = movementSpeed * Time.deltaTime;
        float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint.position);

        if (distanceToWaypoint > minDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
        }
        else
        {
            targetWaypointIndex++;
            UpdateTargetWaypoint();
            ChangeState(State.Idle);
        }

        if (enemyType == EnemyType.FlyEnemy && player != null)
        {
            float playerDistance = Vector3.Distance(transform.position, player.position);

            if (playerDistance <= detectionRange) // Detection range check
            {
                Debug.Log("Player detected!");
                ChangeState(State.Attacking);
            }
        }
    }

    void HandleAttackingState()
    {
        if (player == null) return;

        float playerDistance = Vector3.Distance(transform.position, player.position);

        // If the player moves out of the detection range, return to patrolling
        if (playerDistance > detectionRange)
        {
            Debug.Log("Player lost! Returning to patrol.");
            ChangeState(State.Walking);
            return;
        }

        // If the player is in attack range, attack
        if (playerDistance <= attackRange && attackTimer <= 0f)
        {
            Debug.Log("Attacking player!");
            attackTimer = attackCooldown; // Reset cooldown
        }
    }

    void HandleDyingState()
    {
        Debug.Log("Enemy is dying...");
    }

    void UpdateTargetWaypoint()
    {
        if (targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0; // Loop back to the first waypoint
        }

        targetWaypoint = waypoints[targetWaypointIndex];
    }

    void OnDrawGizmos()
{
    // Draw a line to the current waypoint
    if (targetWaypoint != null)
    {
        Gizmos.color = Color.green; // Colour for the waypoint line
        Gizmos.DrawLine(transform.position, targetWaypoint.position);
    }

    // Draw a line to the player if in detection range
    if (player != null)
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);

        if (playerDistance <= detectionRange)
        {
            Gizmos.color = Color.yellow; // Colour for detection range
            Gizmos.DrawLine(transform.position, player.position);
        }

        // Optionally, draw a sphere around the enemy for the detection and attack ranges
        Gizmos.color = new Color(1, 1, 0, 0.2f); // Yellow for detection range
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = new Color(1, 0, 0, 0.2f); // Red for attack range
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
}
