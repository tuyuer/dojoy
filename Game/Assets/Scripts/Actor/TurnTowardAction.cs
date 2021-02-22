using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTowardAction : ActorAction
{
    public TurnTowardAction()
    {
        actionType = actor_action_state.actor_action_state_turn_toward;
    }

    public override void Update(float deltaTime)
    {
        if (blackboard.actorSense.IsFacingTarget())
        {
            OnExit();
            return;
        }

        if (blackboard.actorSense.Target != null)
        {
            Vector3 offsetToTarget = blackboard.actorSense.Target.transform.position - blackboard.actor.transform.position;
            blackboard.transform.forward = Vector3.Lerp(blackboard.transform.forward, offsetToTarget, deltaTime * 8.0f);
        } 
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        if (CanTriggerAction())
        {
            blackboard.actorState = actor_action_state.actor_action_state_turn_toward;
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
