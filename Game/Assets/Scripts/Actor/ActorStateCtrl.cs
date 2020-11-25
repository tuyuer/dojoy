using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateCtrl
{
    public actor_state actorState;
    public bool isGrounded;
    public float attackComboLeftTime = 0f;

    //attack combo related
    private combo_type comboType = combo_type.combo_type_none;
    private int comboStep = 0;
    private const float attackComboLastTime = 0.3f;

    //a list of states when the player can attack
    private List<actor_state> attackableState = new List<actor_state>
    {
        actor_state.actor_state_idle,
        actor_state.actor_state_walk,
        actor_state.actor_state_run,
        actor_state.actor_state_punch1,
        actor_state.actor_state_punch2,
        actor_state.actor_state_punch3,
        actor_state.actor_state_kick1,
        actor_state.actor_state_kick2,
        actor_state.actor_state_kick3
    };

    //a list of states when the player can move
    private List<actor_state> moveableState = new List<actor_state>
    {
        actor_state.actor_state_idle,
        actor_state.actor_state_walk,
        actor_state.actor_state_run
    };

    //punch names
    public List<string> punchNames = new List<string>
    {
        AnimatorParameter.Punch1,
        AnimatorParameter.Punch2,
        AnimatorParameter.Punch3
    };

    public List<actor_state> punchStates = new List<actor_state>
    {
        actor_state.actor_state_punch1,
        actor_state.actor_state_punch2,
        actor_state.actor_state_punch3,
    };

    //kick names
    public List<string> kickNames = new List<string>
    {
        AnimatorParameter.Kick1,
        AnimatorParameter.Kick2,
        AnimatorParameter.Kick3
    };

    public List<actor_state> kickStates = new List<actor_state>
    {
        actor_state.actor_state_kick1,
        actor_state.actor_state_kick2,
        actor_state.actor_state_kick3,
    };

    public ActorStateCtrl()
    {
        actorState = actor_state.actor_state_idle;
        isGrounded = true;
    }

    public bool IsInAttackableState()
    {   
        if (isGrounded && 
            attackableState.Contains(actorState))
        {
            return true;
        }
        return false;
    }

    public bool IsInMoveableState()
    {
        if (isGrounded &&
            moveableState.Contains(actorState))
        {
            return true;
        }
        return false;
    }

    public void IncreasePunchCombo()
    {
        comboType = combo_type.combo_type_punch;
        comboStep ++;
        if (comboStep >= 3)
        {
            comboStep = 0;
        }
        attackComboLeftTime = attackComboLastTime;
    }

    public void ResetCombo(){
        attackComboLeftTime = GlobalDef.INVALID_VALUE;
        comboStep = 0;
    }

    public int GetComboStep()
    {
        return comboStep;
    }

    public void Breathe(float deltaTime)
    {
        if (attackComboLeftTime <= GlobalDef.INVALID_VALUE)
        {
            return;
        }

        if (attackComboLeftTime < 0)
        {
            ResetCombo();
            return;
        }
        attackComboLeftTime -= deltaTime;
    }

    public bool IsInQuickTurnAngle(float angle)
    {
        if (Mathf.Abs(angle) > 170)
        {
            return true;
        }
        return false;
    }
}
