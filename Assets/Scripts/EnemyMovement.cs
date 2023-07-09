using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Vector3[] PatrolPoints;
    private Vector3 targetPosition, lastSeenLocation;

    [SerializeField] private float viewDistance;
    [SerializeField] private int patrolSpeed, chaseSpeed;

    private bool chasePlayer = false;

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void Patrolling()
    {
    }

    private void FoundPlayer()
    {
    }
}