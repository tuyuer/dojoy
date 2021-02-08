using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : ActorFSMState
{
    public ChasingState()
    {
        fsmState = actor_fsm_state.actor_fsm_state_chasing;
    }

    public override void Update(float deltaTime)
    {
        //target in (alert || insight) range but not in combat range
        if ((actorSense.IsTargetInsight() || actorSense.IsTargetInAlertRange()) &&
            !actorSense.IsTargetInCombatRange())
        {
            blackboard.navMeshAgent.isStopped = false;
            blackboard.navMeshAgent.SetDestination(blackboard.actorSense.Target.transform.position);
        }
        else
        {
            blackboard.navMeshAgent.isStopped = true;
            OnExit();
        }
        Debug.Log("Update:ChasingState");
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
