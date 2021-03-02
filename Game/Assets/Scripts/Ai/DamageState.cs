using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : ActorFSMState
{
    private float lastTime = 0;
    private int nDamageStep = 0;
    public DamageState()
    {
        fsmState = actor_fsm_state.actor_fsm_state_damage;
    }

    public override void Update(float deltaTime)
    {
        AnimatorStateInfo stateInfo = blackboard.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(AnimatorStateName.ActorDamage1) ||
            stateInfo.IsName(AnimatorStateName.ActorDamage2))
        {
            float normalizedTime = stateInfo.normalizedTime;
            if (normalizedTime > 0.95f)
            {
                nDamageStep = 0;
            }
        }

        lastTime -= deltaTime;
        if (lastTime < 0)
        {
            OnExit();
        }
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        base.OnEnter(arrayParamList);
        blackboard.actorBrain.OnDamage(nDamageStep);
        nDamageStep++;
        lastTime = 2.0f;
    }

    public override void OnExit()
    {
        base.OnExit();
        nDamageStep = 0;
        Debug.Log("Damage State OnExit");
    }
}
