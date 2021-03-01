using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : Brain
{
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();

        //replace LocomotionAction to EnemyLocomotionAction
        actionList[actor_action_state.actor_action_state_locomotion] = new EnemyLocomotionAction();
        actionList[actor_action_state.actor_action_state_locomotion].AttachBlackboard(blackboard);
    }

    void Update()
    {
        //input direction
        blackboard.moveDir = transform.forward;
        Vector3 actorSpeed = blackboard.navMeshAgent.velocity;

        //apply gravity
        actorSpeed.y -= GlobalDef.WORLD_GRAVITY * Time.deltaTime;

        //set actorSpeed to Blackboard
        blackboard.actorSpeed = actorSpeed;

        //update active action in actionList
        foreach (KeyValuePair<actor_action_state, ActorAction> kv in actionList)
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

    public override void OnReceiveDamage(ActorAttackInfo atkInfo)
    {
        EnemyBlackboard enemyBlackboard = (EnemyBlackboard)blackboard;
        if (enemyBlackboard)
        {
            ActorFSMMachine fsmMachine = enemyBlackboard.fsmMachine;
            if (fsmMachine)
            {
                enemyBlackboard.attackerAttackInfo = atkInfo;
                fsmMachine.TryTriggerState(actor_fsm_state.actor_fsm_state_damage);
            }
        }
    }
}
