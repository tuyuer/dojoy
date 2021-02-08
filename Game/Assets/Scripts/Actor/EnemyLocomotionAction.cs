using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotionAction : ActorAction
{
    public EnemyLocomotionAction()
    {
        actionType = actor_action_state.actor_action_state_locomotion;
    }

    public override void Update(float deltaTime)
    {
        float forwardSpeed = 0;

        //set character forward direction
        forwardSpeed = blackboard.navMeshAgent.velocity.magnitude;
        if (forwardSpeed > GlobalDef.ACTOR_MAX_FOWARD_SPEED)
        {
            forwardSpeed = GlobalDef.ACTOR_MAX_FOWARD_SPEED;
        }
        animator.SetFloat(AnimatorParameter.ForwardSpeed, forwardSpeed);
    }
}
