using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : Brain
{
    NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<ActorBlackboard>();
        if (blackboard.swordSocket != null)
        {
            blackboard.swordSocket.gameObject.SetActive(true);
        }

        actionList.Add(actor_state.actor_state_locomotion, new EnemyLocomotionAction());
        actionList.Add(actor_state.actor_state_climb, new ClimbAction());
        actionList.Add(actor_state.actor_state_jump, new JumpAction());
        actionList.Add(actor_state.actor_state_land, new LandAction());
        actionList.Add(actor_state.actor_state_vault, new VaultAction());
        actionList.Add(actor_state.actor_state_dodge, new DodgeAction());
        actionList.Add(actor_state.actor_state_punch, new PunchAction());
        actionList.Add(actor_state.actor_state_sword_attack, new SwordAttackAction());

        foreach (KeyValuePair<actor_state, ActorAction> kv in actionList)
        {
            ActorAction actorAction = kv.Value;
            actorAction.AttachBlackboard(blackboard);
        }
    }

    void Update()
    {
        //input direction
        blackboard.moveDir = transform.forward;
        Vector3 actorSpeed = navMeshAgent.velocity;

        //apply gravity
        actorSpeed.y -= GlobalDef.WORLD_GRAVITY * Time.deltaTime;

        //set actorSpeed to Blackboard
        blackboard.actorSpeed = actorSpeed;

        //update active action in actionList
        foreach (KeyValuePair<actor_state, ActorAction> kv in actionList)
        {
            ActorAction actorAction = kv.Value;
            bool isActionEnabled = actorAction.ActionType == blackboard.actorState;
            actorAction.setEnabled(isActionEnabled);
            if (actorAction.IsEnabled)
            {
                actorAction.Update(Time.deltaTime);
            }
        }
    }
}
