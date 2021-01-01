using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBlackboard : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public Transform actor;

    public bool showSword = false;

    public actor_state actorState = actor_state.actor_state_locomotion;
    public Vector3 moveDir = Vector3.zero;
    public Vector3 actorSpeed = Vector3.zero;
}
