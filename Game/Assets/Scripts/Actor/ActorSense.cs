using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSense : MonoBehaviour
{
    public int eyeRange;
    public int eyeDistance;
    public float alertRange;
    public float combatRange;
    public float attackRange = 1.5f;

    protected GameObject target = null;

    public GameObject Target
    {
        get { return target; }
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDrawGizmos()
    {
       
    }

    public bool IsTargetInsight()
    {
        Vector3 positionOffset = target.transform.position - transform.position;
        if (positionOffset.magnitude > eyeDistance)
        {
            return false;
        }

        float angleOffset = Vector3.Angle(transform.forward, positionOffset);
        if (angleOffset > eyeRange / 2)
        {
            return false;
        }

        RaycastHit rayHit;
        if (Physics.Raycast(transform.position + Vector3.up, positionOffset,out rayHit, eyeDistance))
        {
            if (rayHit.collider.CompareTag(TagDef.Player))
            {
                return true;
            }
        }

        return false;
    }

    public bool IsTargetInAlertRange()
    {
        Vector3 offsetToTarget = OffsetToTarget();
        if (offsetToTarget.magnitude < alertRange)
        {
            return true;
        }
        return false;
    }

    public bool IsTargetInCombatRange()
    {
        Vector3 offsetToTarget = OffsetToTarget();
        if (offsetToTarget.magnitude < combatRange)
        {
            return true;
        }
        return false;
    }

    public bool IsTargetInAttackRange()
    {
        Vector3 offsetToTarget = OffsetToTarget();
        if (offsetToTarget.magnitude < attackRange)
        {
            return true;
        }
        return false;
    }

    public bool IsFacingTarget()
    {
        Vector3 toVec = target.transform.position - transform.position;
        Vector3 fromVec = transform.forward;
        float angleOffset = Vector3.Angle(fromVec, toVec);
        return angleOffset < 10;
    }

    public Vector3 OffsetToTarget()
    {
        return target.transform.position - transform.position;
    }

    public float DistanceFromTarget()
    {
        return (target.transform.position - transform.position).magnitude;
    }

    public Vector3 PositionOfAwayFromTarget(float distance)
    {
        Vector3 offsetToTarget = OffsetToTarget();
        return target.transform.position - offsetToTarget.normalized * distance;
    }
}
