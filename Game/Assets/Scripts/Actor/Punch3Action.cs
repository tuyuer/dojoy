using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch3Action : ActorAction
{
    public Punch3Action()
    {
        actionType = actor_state.actor_state_punch3;
    }

    public override void Update(float deltaTime)
    {

    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        if (CanTriggerAction())
        {
            blackboard.animator.SetTrigger(AnimatorParameter.Punch3);
            blackboard.actorState = actor_state.actor_state_punch3;
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
