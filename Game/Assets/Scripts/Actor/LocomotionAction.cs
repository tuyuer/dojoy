using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionAction : ActorAction
{
    public LocomotionAction()
    {
        actionType = actor_action_state.actor_action_state_locomotion;
    }

    public override void Update(float deltaTime)
    {
        float forwardSpeed = 0;
        //set character forward direction
        if (blackboard.moveDir.sqrMagnitude > 0)
        {
            blackboard.actor.forward = blackboard.moveDir;
            forwardSpeed = GlobalDef.ACTOR_MAX_FOWARD_SPEED;
        }

        //animator
        animator.SetFloat(AnimatorParameter.ForwardSpeed, forwardSpeed);

        //move chracter
        if (blackboard.characterController.enabled)
        {
            blackboard.characterController.Move(blackboard.actorSpeed);
        }
    }
}
