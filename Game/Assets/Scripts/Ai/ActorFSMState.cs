using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActorFSMState 
{
    protected ActorBlackboard blackboard;
    protected WayPointManager wayPointManager = null;
    protected ActorSense actorSense = null;

    protected actor_fsm_state fsmState = actor_fsm_state.actor_fsm_state_none;
    public actor_fsm_state FsmState
    {
        get { return fsmState; }
    }

    protected fsm_state_status stateStatus = fsm_state_status.fsm_state_status_waitting;

    public virtual void Update(float deltaTime) { }

    public virtual void OnEnter(ArrayList arrayParamList = null) {
        stateStatus = fsm_state_status.fsm_state_status_running;

        // get and set property
        wayPointManager = blackboard.gameObject.GetComponent<WayPointManager>();
        actorSense = blackboard.actorSense;
    }

    public virtual void OnExit()
    {
        stateStatus = fsm_state_status.fsm_state_status_waitting;
        wayPointManager = null;
        actorSense = null;
    }

    public ActorFSMState()
    {

    }

    public bool IsRunning()
    {
        return stateStatus == fsm_state_status.fsm_state_status_running;
    }

    public void AttachBlackboard(ActorBlackboard blackboard)
    {
        this.blackboard = blackboard;
    }
}
