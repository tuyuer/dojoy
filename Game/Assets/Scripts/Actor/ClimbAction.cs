using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbAction : ActorAction
{
    private bool isAttachingWall = false;
    private float checkDistance = 1.0f;

    public ClimbAction()
    {
        actionType = actor_action_state.actor_action_state_climb;
    }

    public override void Update(float deltaTime)
    {
        Debug.Log("Locomotion Action!!");
        //    if (!isAttachingWall)
        //    {
        //        RaycastHit hitInfo;
        //        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, checkDistance))
        //        {
        //            AttachToWall();
        //        }
        //    }
        //    else
        //    {

        //    }
    }

    //protected override void OnRunning()
    //{
    //    if (!isAttachingWall)
    //    {
    //        RaycastHit hitInfo;
    //        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, checkDistance))
    //        {
    //            AttachToWall();
    //        }
    //    }
    //    else
    //    {

    //    }
    //}

    void AttachToWall()
    {

    }
}
