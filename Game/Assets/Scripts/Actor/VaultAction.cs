using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultAction : ActorAction
{
    private Vector3 matchPoint;
    public VaultAction()
    {
        actionType = actor_action_state.actor_action_state_vault;
    }

    public override void Update(float deltaTime)
    {
        blackboard.animator.applyRootMotion = true;
        blackboard.characterController.enabled = false;

        ProcessMatchTarget();
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        matchPoint = Vector3.zero;
        //param0: matchPoint
        if (arrayParamList!=null)
        {
            matchPoint = (Vector3)arrayParamList[0];
            animator.SetTrigger(AnimatorParameter.Vault);
            blackboard.actorState = actor_action_state.actor_action_state_vault;
        }
    }

    public override void OnExit()
    {
        blackboard.animator.applyRootMotion = false;
        blackboard.characterController.enabled = true;
        blackboard.actorState = actor_action_state.actor_action_state_jump;
    }

    void ProcessMatchTarget()
    {
        if (blackboard.animator.IsInTransition(0))
            return;

        AnimatorStateInfo mState = blackboard.animator.GetCurrentAnimatorStateInfo(0);
        if (mState.IsName(AnimatorStateName.Vault))
        {
            animator.MatchTarget(matchPoint, Quaternion.identity, AvatarTarget.RightHand, new MatchTargetWeightMask(Vector3.one, 0), 0.2f, 0.3f);
        }
    }
}
