using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotionAction : ActorAction
{
    public EnemyLocomotionAction()
    {
        actionType = actor_state.actor_state_locomotion;
    }

    public override void Update(float deltaTime)
    {
        float forwardSpeed = Mathf.Min(blackboard.actorSpeed.magnitude, 2.0f);
        if (forwardSpeed < 0.5f)
        {
            forwardSpeed = 0;
        }
        //animator
        animator.SetFloat(AnimatorParameter.ForwardSpeed, forwardSpeed);
    }
}
