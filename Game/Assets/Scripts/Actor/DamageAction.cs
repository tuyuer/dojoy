using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : ActorAction
{
    public DamageAction()
    {
        actionType = actor_action_state.actor_action_state_damage;
    }

    public override void Update(float deltaTime)
    {
        AnimatorStateInfo stateInfo = blackboard.animator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = stateInfo.normalizedTime;

        if (normalizedTime > 0.9f)
        {
            OnExit();
        }
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        //if (!IsInDamagableState())
        //{
        //    return;
        //}

        animator.SetTrigger(AnimatorParameter.Damage01);
        blackboard.actorState = actor_action_state.actor_action_state_damage;
    }

    public override void OnExit()
    {
        blackboard.actorState = actor_action_state.actor_action_state_locomotion;
    }

    private bool IsInDamagableState()
    {
        List<actor_action_state> damagableState = new List<actor_action_state>
        {
            actor_action_state.actor_action_state_locomotion
        };

        if (damagableState.Contains(blackboard.actorState))
        {
            return true;
        }
        return false;
    }
}
