using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeAction : ActorAction
{
    public DodgeAction()
    {
        actionType = actor_action_state.actor_action_state_dodge;
    }

    public override void Update(float deltaTime)
    {
        //in dodge
        float dodgeSpeed = blackboard.animator.GetFloat(AnimatorParameter.DodgeSpeed);
        if (dodgeSpeed > 0)
        {
            blackboard.actorSpeed += blackboard.actor.forward * dodgeSpeed * GlobalDef.ACTOR_DODGE_SPEED * Time.deltaTime;
        }

        //move chracter
        if (blackboard.characterController.enabled)
        {
            blackboard.characterController.Move(blackboard.actorSpeed);
        }

        AnimatorStateInfo stateInfo = blackboard.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(AnimatorStateName.ActorDodge))
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
        if (CanTriggerAction())
        {
            blackboard.animator.SetTrigger(AnimatorParameter.Dodge);
            blackboard.actorState = actor_action_state.actor_action_state_dodge;
        }
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
}
