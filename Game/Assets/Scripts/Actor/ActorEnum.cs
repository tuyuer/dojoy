
public enum actor_action_state
{
    actor_action_state_locomotion = 0,
    actor_action_state_dodge = 1,
    actor_action_state_jump = 2,
    actor_action_state_land = 10,
    actor_action_state_vault = 11,
    actor_action_state_climb = 12,
    actor_action_state_punch = 13,
    actor_action_state_sword_attack = 14,
    actor_action_state_damage = 15,
};

public enum combo_type
{
    combo_type_none = 0,
    combo_type_punch = 1,
    combo_type_kick = 2,
};

public enum actor_fsm_state
{
    actor_fsm_state_none,
    actor_fsm_state_patrol,
    actor_fsm_state_chasing,
    actor_fsm_state_combat,
    actor_fsm_state_damage,
};

public enum fsm_state_status
{
    fsm_state_status_waitting,
    fsm_state_status_running,
};