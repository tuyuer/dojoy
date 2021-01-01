using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandAction : ActorAction
{
    public LandAction()
    {
        actionType = actor_state.actor_state_land;
    }

    public override void Update(float deltaTime)
    {
        if (blackboard.actorState == actor_state.actor_state_land)
        {
            if (animator.IsInTransition(0))
            {
                //move chracter
                if (blackboard.characterController.enabled)
                {
                    blackboard.characterController.Move(blackboard.actorSpeed);
                }
                return;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(AnimatorStateName.Locomotion))
            {
                OnExit();
            }
        }
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        blackboard.actorState = actor_state.actor_state_land;
        //animator
        animator.SetFloat(AnimatorParameter.ForwardSpeed, 0);
    }

    public override void OnExit()
    {
        blackboard.actorState = actor_state.actor_state_locomotion;
    }
}
