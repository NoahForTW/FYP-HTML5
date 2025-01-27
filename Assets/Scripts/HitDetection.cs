using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private EnemyAI enemyAI;

    private void Start()
    {
        // Find and reference the parent EnemyAI script
        enemyAI = GetComponentInParent<EnemyAI>();

        if (enemyAI == null)
        {
            Debug.LogError("EnemyAI script not found on parent object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyAI != null)
        {
            // Notify the enemy script when the HitBox is triggered
            enemyAI.OnHitDetected(other);
        }
    }
}
