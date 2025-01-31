using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType
    {
        GroundEnemy, // Only moves between waypoints
        FlyEnemy     // Moves and can attack the player
    }

    public enum State
    {
        Idle,        // Enemy is idle
        Walking,     // Enemy is walking between waypoints
        Attacking,   // Enemy is attacking the player
        Dying        // Enemy is dying 
    }

    [SerializeField] private EnemyType enemyType; // Enemy type (Ground or Fly)
    [SerializeField] private State currentState = State.Idle; // Current state
    [SerializeField] private float idleDuration = 2.0f; // Idle time at waypoints
    [SerializeField] private float detectionRange = 5.0f; // Range for detecting the player
    [SerializeField] private float attackRange = 2.0f; // Range for attacking the player
    [SerializeField] private float attackCooldown = 1.5f; // Cooldown between attacks

    [SerializeField] private GameObject hitDetection; // Reference to the HitBox GameObject

    [SerializeField] private float deadBodyTimer;
    private float attackTimer = 0f; // Tracks attack cooldown

    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private int lastWaypointIndex;

    private NavMeshAgent navMeshAgent; // Reference to NavMeshAgent
    private Animator animator; // Reference to Animator
    private bool isIdleCoroutineRunning = false; // Prevent multiple coroutines

    [SerializeField] private Transform player; // Reference to the player

    void Start()
    {
        lastWaypointIndex = waypoints.Count - 1;
        targetWaypoint = waypoints[targetWaypointIndex]; // Set the first waypoint
        animator = GetComponent<Animator>(); // Get Animator
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get NavMeshAgent

        ChangeState(State.Walking); // Start in Walking state
    }

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
                Die();
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
                navMeshAgent.isStopped = true; // Stop the NavMeshAgent
                break;
            case State.Walking:
                animator.SetBool("Walking", true);
                navMeshAgent.isStopped = false; // Resume NavMeshAgent
                break;
            case State.Attacking:
                animator.SetTrigger("Attack");
                navMeshAgent.isStopped = true; // Stop moving while attacking
                break;
            case State.Dying:
                animator.SetTrigger("Die");
                navMeshAgent.isStopped = true;
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
        if (targetWaypoint != null)
        {
            navMeshAgent.SetDestination(targetWaypoint.position);

            // Determine the direction of movement
            Vector3 directionToWaypoint = targetWaypoint.position - transform.position;

            // Flip the enemy based on movement direction
            if (directionToWaypoint.x < 0)
            {
                // Moving right
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (directionToWaypoint.x > 0)
            {
                // Moving left
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            // Check if the enemy reached the waypoint
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                targetWaypointIndex++;
                UpdateTargetWaypoint();
                ChangeState(State.Idle);
            }
        }

        // If player is within detection range, switch to Attacking state
        if (enemyType == EnemyType.FlyEnemy && player != null)
        {
            float playerDistance = Vector3.Distance(transform.position, player.position);

            if (playerDistance <= detectionRange)
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

        // If player moves out of detection range, return to Walking
        if (playerDistance > detectionRange)
        {
            Debug.Log("Player lost! Returning to patrol.");
            ChangeState(State.Walking);
            return;
        }

        // Attack if the player is within range and cooldown is ready
        if (playerDistance <= attackRange && attackTimer <= 0f)
        {
            Debug.Log("Attacking player!");
            attackTimer = attackCooldown; // Reset attack cooldown
        }
        else
        {
            // Face the player while attacking
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void UpdateTargetWaypoint()
    {
        if (targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0; // Loop back to the first waypoint
        }

        targetWaypoint = waypoints[targetWaypointIndex];
    }

    // Called when something interacts with the HitBox
    public void OnHitDetected(Collider other)
    {
        if (other.CompareTag("PlayerFeet"))
        {
            Die(); // Call the Die() method to destroy the enemy
        }
    }

    // Handle enemy death
    private void Die()
    {
        ChangeState(State.Dying);

        // Disable the enemy's collider(s)
        Collider[] colliders = GetComponentsInChildren<Collider>(); // Get all colliders (including children)
        foreach (var collider in colliders)
        {
            collider.enabled = false; // Disable each collider
        }

        // Disable all Rigidbody components (including children)
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = true; // Disable physics simulation
        }

        // Destroy the enemy after a delay to let the death animation play
        Destroy(gameObject, deadBodyTimer);
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
