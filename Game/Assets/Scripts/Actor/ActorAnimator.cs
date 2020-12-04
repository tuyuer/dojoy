﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputComponent))]
public class ActorAnimator : ActorAnimationCallback
{
    private Animator animator;
    private ActorStateCtrl actorStateCtrl;
    private CharacterController characterController;
    private InputComponent inputComponent;
    private Camera mainCamera;

    private Vector3 moveDir = Vector3.zero;             //input dir

    private float jumpSpeed = 0f;

    private float forwardSpeed = 0.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        actorStateCtrl = new ActorStateCtrl(transform);
        characterController = GetComponent<CharacterController>();
        inputComponent = GetComponent<InputComponent>();
        mainCamera = Camera.main;
    }

    void Start()
    {

    }

    void OnEnable()
    {
        inputComponent.onInputEvent += OnInputEvent;
        inputComponent.onDirectionEvent += OnDirectionEvent;
    }

    void OnDisable()
    {
        inputComponent.onInputEvent -= OnInputEvent;
        inputComponent.onDirectionEvent -= OnDirectionEvent;
    }

    // Update is called once per frame
    void Update()
    {
        //breath actor state ctrl
        actorStateCtrl.Breathe(Time.deltaTime);

        //land
        if (CheckActorLand()) return;

        //check falling
        CheckActorFallingStatus();

        //input
        Vector3 targetSpeed = moveDir * GlobalDef.ACTOR_MOVE_SPEED * Time.deltaTime;

        //gravity
        targetSpeed.y -= GlobalDef.WORLD_GRAVITY * Time.deltaTime;

        //in dodge
        float dodgeSpeed = animator.GetFloat(AnimatorParameter.DodgeSpeed);
        if (dodgeSpeed > 0)
        {
            targetSpeed += transform.forward * dodgeSpeed * GlobalDef.ACTOR_DODGE_SPEED * Time.deltaTime;
        }
        
        //in jump
        if (jumpSpeed > 0)
        {
            targetSpeed += transform.up * jumpSpeed * Time.deltaTime;
            jumpSpeed -= Time.deltaTime * GlobalDef.ACTOR_JUMP_SPEED_ACCEL;
        }

        //character move
        characterController.Move(targetSpeed);

        //set character forward direction
        if (moveDir.sqrMagnitude > 0)
        {
            transform.forward = moveDir;
        }
    }

    void CheckActorFallingStatus()
    {
        bool isFalling = false;
        if (characterController.velocity.y < 0.1f)
        {
            isFalling = true;
        }
        if (characterController.isGrounded)
        {
            isFalling = false;
        }
        animator.SetBool(AnimatorParameter.IsFalling, isFalling);
    }

    bool CheckActorLand()
    {
        if (actorStateCtrl.actorState == actor_state.actor_state_land)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(AnimatorStateName.Locomotion))
            {
                actorStateCtrl.actorState = actor_state.actor_state_locomotion;
            }
            return true;
        }
        return false;
    }

    Vector3 CalculateMoveDir(Vector2 dir)
    {
        //得到投影向量 为vector到以planeNormal为法向量的平面上。
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(mainCamera.transform.right, Vector3.up).normalized;

        Vector3 forwardDir = forward * dir.y;
        Vector3 rightDir = right * dir.x;
        return (forwardDir + rightDir).normalized;
    }


    void OnInputEvent(string action, input_action_state actionState)
    {
        if (action == InputActionNames.O && actionState == input_action_state.press)
        {
            OnAttackO();
        }
        else if (action == InputActionNames.X && actionState == input_action_state.press)
        {
            OnAttackX();
        }
        else if (action == InputActionNames.DODGE && actionState == input_action_state.press)
        {
            OnDodge();
        }
        else if (action == InputActionNames.JUMP && actionState == input_action_state.press)
        {
            OnJump();
        }
    }

    void OnDirectionEvent(Vector2 dir, Vector2 dirRaw, input_action_state inputState)
    {
        animator.SetFloat(AnimatorParameter.ForwardSpeed, forwardSpeed);
        if (actorStateCtrl.IsInMoveableState() &&
            (inputState == input_action_state.press || inputState == input_action_state.hold))
        {
            forwardSpeed = GlobalDef.ACTOR_MAX_FOWARD_SPEED;
            moveDir = CalculateMoveDir(dir);
        }
        else if (!characterController.isGrounded )
        {
            moveDir = CalculateMoveDir(dir);
        }
        else if (animator.GetFloat(AnimatorParameter.DodgeSpeed) > 0)
        {
            moveDir = transform.forward;
        }
        else{
            forwardSpeed = 0;
            moveDir = Vector3.zero;
        }
    }

    void OnAttackO()
    {
        if (actorStateCtrl.IsInAttackableState())
        {
            if (actorStateCtrl.kickStates.Contains(actorStateCtrl.actorState))
            {
                return;
            }

            if (actorStateCtrl.actorState == actor_state.actor_state_punch1 &&
                actorStateCtrl.attackComboLeftTime <= 0)
            {
                return;
            }

            if (actorStateCtrl.actorState == actor_state.actor_state_punch2 &&
                actorStateCtrl.attackComboLeftTime <= 0)
            {
                return;
            }

            if (actorStateCtrl.actorState == actor_state.actor_state_punch3)
            {
                return;
            }

            int comboStep = actorStateCtrl.GetComboStep();
            string punchName = actorStateCtrl.punchNames[comboStep];
            actor_state punchState = actorStateCtrl.punchStates[comboStep];
            animator.SetTrigger(punchName);
            actorStateCtrl.actorState = punchState;
            actorStateCtrl.attackComboLeftTime = GlobalDef.INVALID_VALUE;
        }
    }

    void OnAttackX()
    {
        if (actorStateCtrl.IsInAttackableState())
        {
            if (actorStateCtrl.punchStates.Contains(actorStateCtrl.actorState))
            {
                return;
            }

            if (actorStateCtrl.actorState == actor_state.actor_state_kick1 &&
                actorStateCtrl.attackComboLeftTime <= 0)
            {
                return;
            }

            if (actorStateCtrl.actorState == actor_state.actor_state_kick2 &&
                actorStateCtrl.attackComboLeftTime <= 0)
            {
                return;
            }

            if (actorStateCtrl.actorState == actor_state.actor_state_kick3)
            {
                return;
            }

            int comboStep = actorStateCtrl.GetComboStep();
            string kickName = actorStateCtrl.kickNames[comboStep];
            actor_state kickState = actorStateCtrl.kickStates[comboStep];
            animator.SetTrigger(kickName);
            actorStateCtrl.actorState = kickState;
            actorStateCtrl.attackComboLeftTime = GlobalDef.INVALID_VALUE;
        }
    }

    void OnDodge()
    {
        if (actorStateCtrl.IsInAttackableState())
        {
            animator.SetTrigger(AnimatorParameter.Dodge);
            actorStateCtrl.actorState = actor_state.actor_state_dodge;
        }
    }

    void OnJump()
    {
        if (actorStateCtrl.IsInJumpableState())
        {
            animator.SetTrigger(AnimatorParameter.Jump);
            actorStateCtrl.actorState = actor_state.actor_state_jump;
            jumpSpeed = GlobalDef.ACTOR_JUMP_SPEED;
        }
    }

    //override method for listening animation end callback
    public override void OnAnimationEnd(string animationName)
    {
        Debug.Log(animationName + " End");
        if (actorStateCtrl.punchNames.Contains(animationName) ||
            actorStateCtrl.kickNames.Contains(animationName))
        {
            actorStateCtrl.actorState = actor_state.actor_state_locomotion;
        }
        else if (animationName.Equals(AnimatorParameter.Dodge))
        {
            actorStateCtrl.actorState = actor_state.actor_state_locomotion;
        }
    }

    //override method for listening active attack combo
    public override void OnAttackComboBegin(string animationName)
    {
        Debug.Log(animationName + " Combo Begin");
        if (actorStateCtrl.punchNames.Contains(animationName))
        {
            actorStateCtrl.IncreasePunchCombo();
        }
    }

    //override method for quick turn
    public override void OnQuickTurnFinished(string animationName)
    {
        Debug.Log("OnQuickTurnFinished");
        actorStateCtrl.actorState = actor_state.actor_state_locomotion;
        forwardSpeed = GlobalDef.ACTOR_FOWARD_WALK_SPEED;
    }

    //override method for land groud
    public override void OnLandGround()
    {
        Debug.Log("OnLandGround");
        actorStateCtrl.actorState = actor_state.actor_state_land;
    }
}