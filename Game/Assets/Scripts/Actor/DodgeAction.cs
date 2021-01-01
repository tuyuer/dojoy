using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeAction : ActorAction
{
    public DodgeAction()
    {
        actionType = actor_state.actor_state_dodge;
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
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        if (CanTriggerAction())
        {
            blackboard.animator.SetTrigger(AnimatorParameter.Dodge);
            blackboard.actorState = actor_state.actor_state_dodge;
        }
    }

    public override void OnExit()
    {
        blackboard.actorState = actor_state.actor_state_locomotion;
    }

    public override bool CanTriggerAction()
    {
        if (blackboard.characterController.isGrounded &&
            blackboard.actorState == actor_state.actor_state_locomotion)
        {
            return true;
        }
        return false;
    }
}
