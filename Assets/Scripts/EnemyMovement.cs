using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private enum EnemyStates
    {
        patrolling,
        chasing,
        backtracking,
        searching
    }

    [SerializeField] private Vector3[] patrolPoints;
    private List<Vector3> playerPath = new List<Vector3>();
    private Vector3 targetPosition, lastSeenLocation;

    [SerializeField] private float viewDistance, chaseDocumentFrequency;
    [SerializeField] private int patrolSpeed, chaseSpeed, secondsToGiveUpChase;
    private int speed, currentPoint = 0, pointLeft = 0;

    private ViewCone viewCone;

    private bool waiting = false, givingUp = false;
    private SpriteRenderer sr;

    private EnemyStates enemyState = EnemyStates.patrolling;

    private void Start()
    {
        speed = patrolSpeed;
        targetPosition = patrolPoints.Length > 0 ? patrolPoints[0] : transform.position;
        viewCone = GetComponent<ViewCone>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //find move mode
        if (viewCone.targetInRange) //chase player if in range
        {
            if (enemyState != EnemyStates.chasing) //just discovred player
            {
                pointLeft = currentPoint;
            }

            sr.color = Color.red; //debug

            enemyState = EnemyStates.chasing;
            targetPosition = viewCone.lastKnownLocation;
            speed = chaseSpeed;
            if (!waiting) StartCoroutine(SavePath());
        }
        else if (enemyState == EnemyStates.chasing) //giving up
        {
            speed = patrolSpeed;

            sr.color = new Color(1f, 0.5f, 0f, 1f);

            enemyState = EnemyStates.searching;
            targetPosition = transform.position;
            StartCoroutine(GiveUp());
        }
        else if (enemyState == EnemyStates.backtracking) //backtracking
        {
            sr.color = new Color(1, 1, 0f, 1f);

            //move through saved path backwards
            if (transform.position == targetPosition)
            {
                playerPath.RemoveAt(currentPoint);

                //next point
                if (currentPoint != 0) currentPoint--;
                else
                {
                    currentPoint = pointLeft;
                    enemyState = EnemyStates.patrolling;
                    return;
                }
            }
            targetPosition = playerPath[currentPoint];
        }
        else if (enemyState == EnemyStates.patrolling)//patrolling
        {
            sr.color = Color.green;

            if (transform.position == targetPosition)
            {
                //next point
                if (patrolPoints.Length > 0) currentPoint = ++currentPoint % patrolPoints.Length;
            }
            targetPosition = patrolPoints[currentPoint];
            speed = patrolSpeed;
        }

        //move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private IEnumerator SavePath()
    {
        waiting = true;
        playerPath.Add(transform.position);
        yield return new WaitForSecondsRealtime(chaseDocumentFrequency);
        waiting = false;
    }

    private IEnumerator GiveUp()
    {
        yield return new WaitForSecondsRealtime(secondsToGiveUpChase);
        currentPoint = playerPath.Count - 1;
        enemyState = EnemyStates.backtracking;
    }
}