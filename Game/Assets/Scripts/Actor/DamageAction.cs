using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : ActorAction
{
    public DamageAction()
    {
        actionType = actor_action_state.actor_action_state_damage;
    }

    public override void Update(float deltaTime)
    {
        blackboard.characterController.SimpleMove(Vector3.back * 5);
        Debug.Log("isNavEnabled = " + blackboard.navMeshAgent.enabled);

        AnimatorStateInfo stateInfo = blackboard.animator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = stateInfo.normalizedTime;
        if ((stateInfo.IsName(AnimatorStateName.ActorDamage1) || stateInfo.IsName(AnimatorStateName.ActorDamage2)) &&
            normalizedTime > 0.9f)
        {
            OnExit();
        }
    }

    public override void OnEnter(ArrayList arrayParamList = null)
    {
        if (!IsInDamagableState())
        {
            return;
        }
        List<string> attackNames = new List<string>
        {
            "ActorPunch1",
            "ActorPunch2",
            "ActorPunch3",
            "SwordAttack1",
            "SwordAttack2",
            "SwordAttack3",
        };
        foreach (string attackName in attackNames)
        {
            blackboard.animator.SetBool(attackName, false);
        }

        if (arrayParamList != null)
        {
            int nDamageStep = (int)arrayParamList[0];
            if (nDamageStep > 0)
            {
                animator.SetTrigger(AnimatorParameter.Damage02);
            }
            else
            {
                animator.SetTrigger(AnimatorParameter.Damage01);
            }
        }
        blackboard.actorState = actor_action_state.actor_action_state_damage;
    }

    void ReceiveAttack(ActorAttackInfo atkInfo)
    {
        blackboard.characterController.SimpleMove(Vector3.up * 100000);
        Debug.Log("ReceiveAttack .. ");
    }

    public override void OnExit()
    {
        blackboard.actorState = actor_action_state.actor_action_state_locomotion;
    }

    private bool IsInDamagableState()
    {
        return true;
        //List<actor_action_state> damagableState = new List<actor_action_state>
        //{
        //    actor_action_state.actor_action_state_locomotion
        //};

        //if (damagableState.Contains(blackboard.actorState))
        //{
        //    return true;
        //}
        //return false;
    }
}
