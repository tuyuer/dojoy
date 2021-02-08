using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : ActorFSMState
{
    public CombatState()
    {
        fsmState = actor_fsm_state.actor_fsm_state_combat;
    }

    public override void Update(float deltaTime)
    {
        //target in combat range
        if (actorSense.IsTargetInCombatRange())
        {
            blackboard.navMeshAgent.isStopped = false;
            blackboard.navMeshAgent.SetDestination(blackboard.actorSense.Target.transform.position);
        }
        else
        {
            blackboard.navMeshAgent.isStopped = true;
            OnExit();
        }
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        base.OnEnter(arrayParamList);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
