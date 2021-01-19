using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySense : ActorSense
{
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag(TagDef.Player);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Color originGizmoColor = Gizmos.color;
        Gizmos.color = Color.red;

        Vector3 axis = Vector3.up;
        Vector3 direction = transform.forward;
        Vector3 origin = transform.position;
        float radius = eyeDistance;
        float angle = eyeRange;

        Vector3 leftdir = Quaternion.AngleAxis(-angle / 2, axis) * direction;
        Vector3 rightdir = Quaternion.AngleAxis(angle / 2, axis) * direction;

        Vector3 currentP = origin + leftdir * radius;
        Vector3 oldP;
        if (angle != 360)
        {
            Gizmos.DrawLine(origin, currentP);
        }
        for (int i = 0; i < angle / 10; i++)
        {
            Vector3 dir = Quaternion.AngleAxis(10 * i, axis) * leftdir;
            oldP = currentP;
            currentP = origin + dir * radius;
            Gizmos.DrawLine(oldP, currentP);
        }
        oldP = currentP;
        currentP = origin + rightdir * radius;
        Gizmos.DrawLine(oldP, currentP);
        if (angle != 360)
        {
            Gizmos.DrawLine(currentP, origin);
        }

        currentP = origin + direction * alertRange;
        Gizmos.color = Color.yellow;
        for (int i = 1; i <= 360; i++)
        {
            Vector3 dir = Quaternion.AngleAxis(2 * i, axis) * direction;
            oldP = currentP;
            currentP = origin + dir * alertRange;
            Gizmos.DrawLine(oldP, currentP);
        }

        currentP = origin + direction * combatRange;
        Gizmos.color = Color.red;
        for (int i = 1; i <= 360; i++)
        {
            Vector3 dir = Quaternion.AngleAxis(2 * i, axis) * direction;
            oldP = currentP;
            currentP = origin + dir * combatRange;
            Gizmos.DrawLine(oldP, currentP);
        }

        Gizmos.color = originGizmoColor;
    }
}
