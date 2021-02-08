using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : ActorFSMState
{
    private const int MAX_COMBO_STEP = 3;
    private int nAttackStep;

    private float fWaitTime = 0.0f;

    public CombatState()
    {
        fsmState = actor_fsm_state.actor_fsm_state_combat;
    }

    public override void Update(float deltaTime)
    {
        if (fWaitTime > 0)
        {
            fWaitTime -= deltaTime;
            return;
        }

        //target in combat range
        if (actorSense.IsTargetInAttackRange())
        {
            blackboard.navMeshAgent.isStopped = true;
            blackboard.actorBrain.OnAttackO();
            fWaitTime = 1.0f;
            nAttackStep++;
        }
        else if (actorSense.IsTargetInCombatRange())
        {
            blackboard.navMeshAgent.isStopped = false;
            blackboard.navMeshAgent.SetDestination(blackboard.actorSense.Target.transform.position);
        }
        else
        {
            blackboard.navMeshAgent.isStopped = true;
            OnExit();
        }
        Debug.Log("Update:CombatState");
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        base.OnEnter(arrayParamList);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    void MakingDecision()
    {

    }
}
