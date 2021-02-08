using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : ActorFSMState
{
    public DamageState()
    {
        fsmState = actor_fsm_state.actor_fsm_state_damage;
    }
}
