using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : ActorFSMState
{
    private const float MAX_THINKING_TIME = 3.0f;
    private const float MIN_THINKING_TIME = 1.0f;

    private float fThinkingLeftTime = 0.0f;
    public Vector3 patrolDestination = Vector3.zero;

    public PatrolState()
    {
        fsmState = actor_fsm_state.actor_fsm_state_patrol;
    }

    public override void Update(float deltaTime)
    {
        fThinkingLeftTime -= Time.deltaTime;
        if (fThinkingLeftTime < GlobalDef.ZERO_VALUE)
        {
            //not found target
            if (!actorSense.IsTargetInsight() &&
                !actorSense.IsTargetInAlertRange())
            {
                blackboard.navMeshAgent.isStopped = false;
                blackboard.navMeshAgent.SetDestination(patrolDestination);
                if ((blackboard.actor.transform.position - patrolDestination).magnitude < 0.2f)
                {
                    blackboard.navMeshAgent.isStopped = true;
                    patrolDestination = wayPointManager.RandomPatrolPoint();
                    fThinkingLeftTime = Random.Range(MIN_THINKING_TIME, MAX_THINKING_TIME);
                }
            }
            else
            {
                blackboard.navMeshAgent.isStopped = true;
                OnExit();
            }
        }
        Debug.Log("Update:PatrolState");
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        base.OnEnter(arrayParamList);
        patrolDestination = wayPointManager.RandomPatrolPoint();
        fThinkingLeftTime = Random.Range(MIN_THINKING_TIME, MAX_THINKING_TIME);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
