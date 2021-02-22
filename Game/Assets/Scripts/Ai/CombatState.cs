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
            if (!blackboard.navMeshAgent.isStopped)
            {
                blackboard.navMeshAgent.isStopped = true;
                fWaitTime = Random.Range(0.5f, 2.0f);
                return;
            }

            if (blackboard.actorState != actor_action_state.actor_action_state_turn_toward &&
                !actorSense.IsFacingTarget())
            {
                blackboard.actorBrain.TurnTwardsTarget();
                return;
            }

            blackboard.actorBrain.OnAttackO();
            fWaitTime = 0.2f;
            nAttackStep++;

            if (nAttackStep % 3 == 0)
            {
                nAttackStep = 0;
                fWaitTime = 2.0f;
            }
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
