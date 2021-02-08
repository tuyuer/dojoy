using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : ActorAction
{
    private float jumpSpeed = 0;
    public JumpAction()
    {
        actionType = actor_action_state.actor_action_state_jump;
    }

    public override void Update(float deltaTime)
    {
        //check falling
        bool isFalling = false;
        if (characterController.velocity.y < 0.1f)
        {
            isFalling = true;
        }
        if (characterController.isGrounded)
        {
            isFalling = false;
        }
        animator.SetBool(AnimatorParameter.IsFalling, isFalling);

        if (jumpSpeed > 0)
        {
            Vector3 actorSpeed = blackboard.actorSpeed;
            actorSpeed += blackboard.actor.up * jumpSpeed * deltaTime;
            jumpSpeed -= deltaTime * GlobalDef.ACTOR_JUMP_SPEED_ACCEL;

            blackboard.actorSpeed = actorSpeed;
        }

        //set character forward direction
        if (blackboard.moveDir.sqrMagnitude > 0)
        {
            blackboard.actor.forward = blackboard.moveDir;
        }

        //move chracter
        if (blackboard.characterController.enabled)
        {
            blackboard.characterController.Move(blackboard.actorSpeed);
        }
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        if (!IsInJumpableState())
        {
            return;
        }

        jumpSpeed = GlobalDef.ACTOR_JUMP_SPEED;
        animator.SetTrigger(AnimatorParameter.Jump);
        blackboard.actorState = actor_action_state.actor_action_state_jump;
    }

    public override void OnExit()
    {
    }

    private bool IsInJumpableState()
    {
        List<actor_action_state> jumpableState = new List<actor_action_state>
        {
            actor_action_state.actor_action_state_locomotion
        };

        if (jumpableState.Contains(blackboard.actorState))
        {
            return true;
        }
        return false;
    }
}
