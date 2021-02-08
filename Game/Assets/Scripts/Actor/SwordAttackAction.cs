using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordAttackAction : ActorAction
{
    private int attackStep = 0;
    private float animationFinishNormalizedTime = 0.95f;
    private int autoTriggerNextStep = -1;
    private float autoTriggerNextConsumeTime = 0.5f;
    private float autoTriggerNextElapsedTime = 0f;

    private List<string> attackNames = new List<string>
    {
        "SwordAttack1",
        "SwordAttack2",
        "SwordAttack3"
    };

    public SwordAttackAction()
    {
        actionType = actor_action_state.actor_action_state_sword_attack;
    }

    public override void Update(float deltaTime)
    {
        if (autoTriggerNextElapsedTime > 0)
        {
            autoTriggerNextElapsedTime -= deltaTime;
            return;
        }

        AnimatorStateInfo stateInfo = blackboard.animator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = stateInfo.normalizedTime;
        if (autoTriggerNextStep > attackStep) //only combo big attackStep
        {
            if (stateInfo.IsName(attackNames[attackStep]))
            {
                if (normalizedTime > 0.9f)
                {
                    TriggerStep(autoTriggerNextStep);
                    attackStep = autoTriggerNextStep;
                    autoTriggerNextElapsedTime = autoTriggerNextConsumeTime;
                }
            }
        }
        else
        {
            if (stateInfo.IsName(attackNames[attackStep]))
            {
                if (normalizedTime > animationFinishNormalizedTime)
                {
                    OnExit();
                }
            }
        }
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        if (CanTriggerAction())
        {
            if (blackboard.actorState == actor_action_state.actor_action_state_sword_attack)
            {
                AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (animatorStateInfo.normalizedTime > 0.3f)
                {
                    int nNextStep = GetNextAttackStep();
                    autoTriggerNextStep = nNextStep;
                }
            }
            else
            {
                TriggerStep(attackStep);
                blackboard.actorState = actor_action_state.actor_action_state_sword_attack;
                autoTriggerNextStep = attackStep;
            }
        }
    }

    public override void OnExit()
    {
        attackStep = 0;
        autoTriggerNextStep = -1;
        ClearTriggers();
        blackboard.actorState = actor_action_state.actor_action_state_locomotion;
        Debug.Log("swordAttackAction OnExit()");
    }

    public override bool CanTriggerAction()
    {
        if (blackboard.actorBrain.IsGrounded() &&
            (blackboard.actorState == actor_action_state.actor_action_state_locomotion ||
            blackboard.actorState == actor_action_state.actor_action_state_sword_attack))
        {
            return true;
        }
        return false;
    }

    private int GetNextAttackStep()
    {
        int nNextStep = attackStep + 1;
        return nNextStep % 3;
    }

    private void TriggerStep(int nStep)
    {
        for (int i = 0; i < attackNames.Count; i++)
        {
            blackboard.animator.SetBool(attackNames[i], i == nStep);
        }
        blackboard.sowrdEffectSocket.PlayEfect(nStep);
        blackboard.attackRange.ActivateWithTime(0.6f);
    }

    private void ClearTriggers()
    {
        for (int i = 0; i < attackNames.Count; i++)
        {
            blackboard.animator.SetBool(attackNames[i], false);
        }
    }
}
