using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType
    {
        GroundEnemy, // Only moves between waypoints using NavMesh
        FlyEnemy     // Moves between waypoints using custom code
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
    [SerializeField] private float flySpeed = 3.0f; // Speed for flying enemy movement
    [SerializeField] private float attackSpeed = 6.0f; // Increased speed during attack
    [SerializeField] private float explosionRadius = 5.0f; // Radius for flying enemy explosion
    [SerializeField] private int explosionDamage = 2; // Damage dealt by flying enemy explosion
    [SerializeField] private int enemyDamage = 1; // Damage dealt by flying enemy explosion
    [SerializeField] private ParticleSystem explosionEffect;

    [SerializeField] private GameObject hitDetection; // Reference to the HitBox GameObject

    [SerializeField] private float deadBodyTimer;
    private float attackTimer = 0f; // Tracks attack cooldown

    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private int lastWaypointIndex;

    private NavMeshAgent navMeshAgent; // Reference to NavMeshAgent (for ground enemy)
    private Animator animator; // Reference to Animator
    private bool isIdleCoroutineRunning = false; // Prevent multiple coroutines

    [SerializeField] private Transform player; // Reference to the player

    private void Start()
    {
        lastWaypointIndex = waypoints.Count - 1;
        targetWaypoint = waypoints[targetWaypointIndex]; // Set the first waypoint
        animator = GetComponent<Animator>(); // Get Animator

        // Only initialize NavMeshAgent for ground enemy
        if (enemyType == EnemyType.GroundEnemy)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        ChangeState(State.Walking); // Start in Walking state
    }

    private void Update()
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
                HandleAttackingState();
                break;
            case State.Dying:
                Die();
                break;
        }
    }

    private void ChangeState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.Idle:
                animator.SetBool("Walking", false);
                if (enemyType == EnemyType.GroundEnemy)
                {
                    navMeshAgent.isStopped = true; // Stop the NavMeshAgent
                }
                break;
            case State.Walking:
                animator.SetBool("Walking", true);
                if (enemyType == EnemyType.GroundEnemy)
                {
                    navMeshAgent.isStopped = false; // Resume NavMeshAgent
                }
                break;
            case State.Attacking:
                animator.SetTrigger("Attack");
                animator.SetBool("Walking", false);
                break;
            case State.Dying:
                animator.SetTrigger("Die");
                if (enemyType == EnemyType.GroundEnemy)
                {
                    navMeshAgent.isStopped = true;
                }
                break;
        }
    }

    private void HandleIdleState()
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

    private void HandleWalkingState()
    {
        if (enemyType == EnemyType.GroundEnemy)
        {
            HandleGroundEnemyMovement();
        }
        else if (enemyType == EnemyType.FlyEnemy)
        {
            HandleFlyingEnemyMovement();
        }

        // Check for player detection
        if (player != null)
        {
            float playerDistance = Vector3.Distance(transform.position, player.position);

            if (playerDistance <= detectionRange)
            {
                Debug.Log("Player detected!");
                ChangeState(State.Attacking);
            }
        }
    }

    private void HandleGroundEnemyMovement()
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
    }

    private void HandleFlyingEnemyMovement()
    {
        if (targetWaypoint != null)
        {
            // Move towards the target waypoint
            float currentSpeed = (currentState == State.Attacking) ? attackSpeed : flySpeed;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, currentSpeed * Time.deltaTime);

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
            if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.1f)
            {
                targetWaypointIndex++;
                UpdateTargetWaypoint();
                ChangeState(State.Idle);
            }
        }
    }

    private void HandleAttackingState()
    {
        if (player == null) 
        {
            return;
        }

        float playerDistance = Vector3.Distance(transform.position, player.position);

        // If player moves out of detection range, return to Walking
        if (playerDistance > detectionRange)
        {
            Debug.Log("Player lost! Returning to patrol.");
            ChangeState(State.Walking);
            return;
        }

        // Face the player while attacking
        Vector3 direction = (player.position - transform.position).normalized;
        if (direction.x > 0)
        {
            // Player is to the left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            // Player is to the right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Attack if the player is within range and cooldown is ready
        if (playerDistance <= attackRange && attackTimer <= 0f)
        {
            Debug.Log("Attacking player!");
            attackTimer = attackCooldown; // Reset attack cooldown
        }
        else
        {
            // Move towards the player (for flying enemy)
            if (enemyType == EnemyType.FlyEnemy)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, attackSpeed * Time.deltaTime);
            }
        }
    }

    private void UpdateTargetWaypoint()
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
        
        /*
            Here to change it to hide 
        */
        // Destroy the parent GameObject if it exists
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject, deadBodyTimer);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (enemyType == EnemyType.GroundEnemy)
            {
                // Remove a heart from the player
                HealthBar.instance.RemoveHearts(enemyDamage);
                Debug.Log("Player lost a heart!");
            }
            else if (enemyType == EnemyType.FlyEnemy)
            {
                // Explode and deal damage to the player if within radius
                Explode();
            }
        }
    }

    private void Explode()
    {
        // Check if the player is within the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // Deal damage to the player
                HealthBar.instance.RemoveHearts(explosionDamage);
            }
        }

        // Instantiate the explosion effect
        ParticleSystem explosionInstance = Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Get the duration of the particle effect
        float explosionDuration = explosionInstance.main.duration;

        // Destroy the particle effect GameObject after it has finished playing
        Destroy(explosionInstance.gameObject, explosionDuration);

        // Destroy the flying enemy
        Die();
    }

    private void OnDrawGizmos()
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

        // Draw the explosion radius for flying enemies
        if (enemyType == EnemyType.FlyEnemy)
        {
            Gizmos.color = new Color(1, 0, 0, 0.1f); // Red for explosion radius
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}