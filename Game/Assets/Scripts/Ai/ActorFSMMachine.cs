using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFSMMachine : MonoBehaviour
{
    protected ActorBlackboard blackboard;
    protected ActorSense actorSense;

    protected Dictionary<actor_fsm_state, ActorFSMState> stateList = new Dictionary<actor_fsm_state, ActorFSMState>();

    private void Awake()
    {
        blackboard = GetComponent<ActorBlackboard>();
        actorSense = blackboard.actorSense;

        stateList.Add(actor_fsm_state.actor_fsm_state_patrol, new PatrolState());
        stateList.Add(actor_fsm_state.actor_fsm_state_chasing, new ChasingState());
        stateList.Add(actor_fsm_state.actor_fsm_state_combat, new CombatState());
        stateList.Add(actor_fsm_state.actor_fsm_state_damage, new DamageState());

        foreach (KeyValuePair<actor_fsm_state, ActorFSMState> kv in stateList)
        {
            ActorFSMState actorFsmState = kv.Value;
            actorFsmState.AttachBlackboard(blackboard);
        }
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool bHasRunningState = HasRunningState();
        if (bHasRunningState)
        {
            LoopRunningState();
        }
        else
        {
            MakingDecision();
        }
    }

    void MakingDecision()
    {
        if (actorSense.IsTargetInCombatRange())
        {
            TryTriggerState(actor_fsm_state.actor_fsm_state_combat);
        }
        else if (actorSense.IsTargetInAlertRange() ||
                 actorSense.IsTargetInsight())
        {
            TryTriggerState(actor_fsm_state.actor_fsm_state_chasing);
        }
        else
        {
            TryTriggerState(actor_fsm_state.actor_fsm_state_patrol);
        }
    }

    void LoopRunningState()
    {
        //update active action in actionList
        foreach (KeyValuePair<actor_fsm_state, ActorFSMState> kv in stateList)
        {
            ActorFSMState actorFsmState = kv.Value;
            if (actorFsmState.IsRunning())
            {
                actorFsmState.Update(Time.deltaTime);
            }
        }
    }

    bool HasRunningState()
    {
        bool bHasRunningState = false;
        foreach (KeyValuePair<actor_fsm_state, ActorFSMState> kv in stateList)
        {
            ActorFSMState actorFsmState = kv.Value;
            if (actorFsmState.IsRunning())
            {
                bHasRunningState = true;
                break;
            }
        }
        return bHasRunningState;
    }

    ActorFSMState GetRunningState()
    {
        foreach (KeyValuePair<actor_fsm_state, ActorFSMState> kv in stateList)
        {
            ActorFSMState actorFsmState = kv.Value;
            if (actorFsmState.IsRunning())
            {
                return actorFsmState;
            }
        }
        return null;
    }

    public void TryTriggerState(actor_fsm_state fsmState)
    {
        switch (fsmState)
        {
            case actor_fsm_state.actor_fsm_state_patrol:
                TryTriggerPatrolState();
                break;
            case actor_fsm_state.actor_fsm_state_chasing:
                TryTriggerChasingState();
                break;
            case actor_fsm_state.actor_fsm_state_combat:
                TryTriggerCombatState();
                break;
            case actor_fsm_state.actor_fsm_state_damage:
                TryTriggerDamageState();
                break;
            default:
                break;
        }
    }

    void TryTriggerPatrolState()
    {
        if (!actorSense.IsTargetInAlertRange() &&
            !actorSense.IsTargetInsight())
        {
            ActorFSMState runningState = GetRunningState();
            if (runningState != null &&
                runningState.FsmState != actor_fsm_state.actor_fsm_state_patrol)
            {
                runningState.OnExit();
            }
            stateList[actor_fsm_state.actor_fsm_state_patrol].OnEnter();
        }
    }

    void TryTriggerChasingState()
    {
        if (actorSense.IsTargetInAlertRange() ||
            actorSense.IsTargetInsight())
        {
            ActorFSMState runningState = GetRunningState();
            if (runningState != null &&
                runningState.FsmState != actor_fsm_state.actor_fsm_state_chasing)
            {
                runningState.OnExit();
            }
            stateList[actor_fsm_state.actor_fsm_state_chasing].OnEnter();
        }
    }

    void TryTriggerCombatState()
    {
        if (actorSense.IsTargetInCombatRange())
        {
            ActorFSMState runningState = GetRunningState();
            if (runningState != null &&
                runningState.FsmState != actor_fsm_state.actor_fsm_state_combat)
            {
                runningState.OnExit();
            }
            stateList[actor_fsm_state.actor_fsm_state_combat].OnEnter();
        }
    }

    void TryTriggerDamageState()
    {
        ActorFSMState runningState = GetRunningState();
        if (runningState != null &&
            runningState.FsmState != actor_fsm_state.actor_fsm_state_damage)
        {
            runningState.OnExit();
        }
        stateList[actor_fsm_state.actor_fsm_state_damage].OnEnter();
    }
}
