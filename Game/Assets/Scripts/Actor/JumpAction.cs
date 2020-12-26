using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : ActorAction
{
    private float jumpSpeed = 0;
    public JumpAction()
    {
        actionType = actor_state.actor_state_jump;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        float forwardSpeed = 0;
        //set character forward direction
        if (blackboard.moveDir.sqrMagnitude > 0)
        {
            blackboard.actor.forward = blackboard.moveDir;
            forwardSpeed = GlobalDef.ACTOR_MAX_FOWARD_SPEED;
        }

        //animator
        animator.SetFloat(AnimatorParameter.ForwardSpeed, forwardSpeed);
    }

    public override void OnEnter()
    {
        base.OnEnter();

        jumpSpeed = GlobalDef.ACTOR_JUMP_SPEED;
        animator.SetTrigger(AnimatorParameter.Jump);
        blackboard.actorState = actor_state.actor_state_jump;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
