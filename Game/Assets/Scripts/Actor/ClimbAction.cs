using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbAction : ActorAction
{
    private bool isAttachingWall = false;
    private float checkDistance = 1.0f;

    void Awake()
    {
    }

    public override void OnRunning()
    {
        if (!isAttachingWall)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, checkDistance))
            {
                AttachToWall();
            }
        }
        else
        {

        }
    }

    void AttachToWall()
    {

    }

    void OnDrawGizmos()
    {
        Color originColor = Gizmos.color;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, .25f);
        Gizmos.color = originColor;
    }
}
