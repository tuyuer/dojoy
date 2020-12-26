using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultAction : ActorAction
{
    public VaultAction()
    {
        actionType = actor_state.actor_state_vault;
    }

    public override void Update(float deltaTime)
    {
        if (blackboard.actorState == actor_state.actor_state_vault)
        {
           
        }
    }

    public override void OnEnter()
    {
        animator.SetTrigger(AnimatorParameter.Vault);
        blackboard.actorState = actor_state.actor_state_vault;
    }

    public override void OnExit()
    {
        //blackboard.actorState = actor_state.actor_state_locomotion;
    }
}
