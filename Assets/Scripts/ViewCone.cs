using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCone : MonoBehaviour
{
    public float viewRadius;

    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public Vector3 lastKnownLocation;
    public bool targetInRange = false;

    private void FixedUpdate()
    {
        FindVisibleTargets();
    }

    private void FindVisibleTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    //player is detected
                    lastKnownLocation = target.position;
                    targetInRange = true;
                    Debug.Log("I see player");
                }
                else
                {
                    targetInRange = false;
                    Debug.Log("player lost 1");
                }
            }
            else
            {
                targetInRange = false;
                Debug.Log("player lost 2");
            }
        }
        if (targetsInViewRadius.Length == 0)
        {
            targetInRange = false;
            Debug.Log("player lost 3");
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}