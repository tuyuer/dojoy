﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch1Action : ActorAction
{
    public Punch1Action()
    {
        actionType = actor_state.actor_state_punch1;
    }

    public override void Update(float deltaTime)
    {

    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        if (CanTriggerAction())
        {
            blackboard.animator.SetTrigger(AnimatorParameter.Punch1);
            blackboard.actorState = actor_state.actor_state_punch1;
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
