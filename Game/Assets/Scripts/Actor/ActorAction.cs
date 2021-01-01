using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAction
{
    protected Animator animator;
    protected CharacterController characterController;
    protected ActorBlackboard blackboard;

    protected bool applyRootMotion = false;
    protected bool enableCharacterController = true;

    protected bool actionEnabled = false;

    protected actor_state actionType = actor_state.actor_state_locomotion;

    public virtual void Update(float deltaTime) { }
    public virtual void OnEnter(ArrayList arrayParamList = null) { }
    public virtual void OnExit() { }
    public virtual bool CanTriggerAction() { return true; }

    public ActorAction()
    {

    }

    public actor_state ActionType
    {
        get { return actionType; }
    }

    public bool IsEnabled
    {
        get { return actionEnabled; }
    }

    public void setEnabled(bool bEnabled)
    {
        actionEnabled = bEnabled;
    }

    public void AttachBlackboard(ActorBlackboard blackboard)
    {
        this.blackboard = blackboard;
        this.animator = blackboard.animator;
        this.characterController = blackboard.characterController;
    }

}
