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

    bool TryTriggerState(actor_fsm_state fsmState)
    {
        stateList[fsmState].OnEnter();
        return false;
    }
}
