using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorBlackboard : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public Transform actor;

    public ActorSense actorSense;
    public AttackRange attackRange;

    public Transform swordSocket;
    public SwordEffectSocket sowrdEffectSocket;

    public NavMeshAgent navMeshAgent;

    public bool showSword = false;

    public actor_action_state actorState = actor_action_state.actor_action_state_locomotion;
    public Vector3 moveDir = Vector3.zero;
    public Vector3 actorSpeed = Vector3.zero;

    //moving actor by navmeshAngent
    public Vector3 navDestination = Vector3.zero;
}
