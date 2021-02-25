using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUpAction : ActorAction
{
    private float elapsedTime = 0;
    private float jumpStartTime = 0.3f;
    private bool jumpTriggerd = false;
    private float jumpSpeed = 0;
    public AttackUpAction()
    {
        actionType = actor_action_state.actor_action_state_attack_up;
    }

    public override void Update(float deltaTime)
    {
        elapsedTime += deltaTime;
        if (!jumpTriggerd &&
            elapsedTime > jumpStartTime)
        {
            jumpSpeed = GlobalDef.ACTOR_SWORD_ATTACKUP_SPEED;
            jumpTriggerd = true;
        }

        if (!jumpTriggerd)
        {
            return;
        }

        if (jumpSpeed > 0)
        {
            Vector3 actorSpeed = blackboard.actorSpeed;
            actorSpeed.x = blackboard.actor.forward.x * deltaTime * 2;
            actorSpeed.z = blackboard.actor.forward.z * deltaTime * 2;
            actorSpeed += blackboard.actor.up * jumpSpeed * deltaTime;
            jumpSpeed -= deltaTime * GlobalDef.ACTOR_JUMP_SPEED_ACCEL;

            blackboard.actorSpeed = actorSpeed;
        }

        //move chracter
        if (blackboard.characterController.enabled)
        {
            blackboard.characterController.Move(blackboard.actorSpeed);
        }

        AnimatorStateInfo stateInfo = blackboard.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(AnimatorStateName.AttackUp))
        {
            float normalizedTime = stateInfo.normalizedTime;
            if (normalizedTime > 0.9f)
            {
                OnExit();
            }
        }
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        if (!IsInAttackUpableState())
        {
            return;
        }

        jumpSpeed = 0;
        jumpTriggerd = false;
        elapsedTime = 0;
        blackboard.moveDir = Vector3.zero;
        animator.SetTrigger(AnimatorParameter.AttackUp);
        blackboard.actorState = actor_action_state.actor_action_state_attack_up;
    }

    public override void OnExit()
    {
        blackboard.actorState = actor_action_state.actor_action_state_locomotion;
    }

    public override bool CanTriggerAction()
    {
        if (blackboard.actorBrain.IsGrounded() &&
            blackboard.actorState == actor_action_state.actor_action_state_locomotion)
        {
            return true;
        }
        return false;
    }

    private bool IsInAttackUpableState()
    {
        List<actor_action_state> attackUpableState = new List<actor_action_state>
        {
            actor_action_state.actor_action_state_locomotion
        };

        if (attackUpableState.Contains(blackboard.actorState))
        {
            return true;
        }
        return false;
    }
}
