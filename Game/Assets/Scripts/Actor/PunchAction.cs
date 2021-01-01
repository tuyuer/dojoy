using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAction : ActorAction
{
    private int attackStep = 0;
    private float attackCombTime = 0.5f;
    private float animationFinishNormalizedTime = 0.9f;

    private List<string> attackNames = new List<string>
    {
        AnimatorParameter.Punch1,
        AnimatorParameter.Punch2,
        AnimatorParameter.Punch3
    };

    private List<KeyCodeEvent> punchEventList = new List<KeyCodeEvent>();

    public PunchAction()
    {
        actionType = actor_state.actor_state_punch;
    }

    public override void Update(float deltaTime)
    {
        AnimatorStateInfo animatorStateInfo = blackboard.animator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = animatorStateInfo.normalizedTime;
        if (animator.IsInTransition(0) &&
            normalizedTime >= animationFinishNormalizedTime)
        {
            if (punchEventList.Count > 0)
            {
                //auto trigger next punch
                blackboard.animator.SetTrigger(attackNames[attackStep]);
                attackStep++;
                attackStep = attackStep % 3;
                punchEventList.Clear();
            }
            else
            {
                OnExit();
            }
        }
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        if (CanTriggerAction())
        {
            if (blackboard.actorState == actor_state.actor_state_punch)
            {
                if (punchEventList.Count == 0)
                {
                    AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                    if (animatorStateInfo.normalizedTime > 0.5f)
                    {
                        punchEventList.Add(new KeyCodeEvent());
                    }
                }
            }
            else
            {
                blackboard.animator.SetTrigger(attackNames[attackStep]);
                blackboard.actorState = actor_state.actor_state_punch;
                attackStep++;
                attackStep = attackStep % 3;
            }
        }
    }

    public override void OnExit()
    {
        blackboard.actorState = actor_state.actor_state_locomotion;
        Debug.Log("punchAction OnExit()");
    }

    public override bool CanTriggerAction()
    {
        if (blackboard.characterController.isGrounded &&
            blackboard.actorState == actor_state.actor_state_locomotion)
        {
            return true;
        }
        return false;
    }
}
