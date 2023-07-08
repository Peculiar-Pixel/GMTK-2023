using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 LastSeenLocation;
    [SerializeField] private Vector3[] PatrolPoints;
    [SerializeField] private float viewDistance;

    private CompositeCollider2D myCone;

    void Start()
    {
        myCone = GetComponentInChildren<CompositeCollider2D>();
    }

    void Update()
    {
        
    }

    private void Patrolling()
    {

    }

    private void FoundPlayer()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Name");
    }
}
