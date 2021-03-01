using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBrain : Brain, IActorAttackCallback
{
    private Camera mainCamera;
    private InputComponent inputComponent;
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
        inputComponent = GetComponent<InputComponent>();
    }

    void OnEnable()
    {
        if (inputComponent != null)
        {
            inputComponent.onInputEvent += OnInputEvent;
            inputComponent.onDirectionEvent += OnDirectionEvent;
        }
    }

    void OnDisable()
    {
        if (inputComponent != null)
        {
            inputComponent.onInputEvent -= OnInputEvent;
            inputComponent.onDirectionEvent -= OnDirectionEvent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //input direction
        Vector3 actorSpeed = blackboard.moveDir * GlobalDef.ACTOR_MOVE_SPEED * Time.deltaTime;

        //apply gravity
        actorSpeed.y -= GlobalDef.WORLD_GRAVITY * Time.deltaTime;

        //set actorSpeed to blackboard
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


    void OnInputEvent(string action, input_action_state actionState)
    {
        if (action == InputActionNames.JUMP && actionState == input_action_state.press)
        {
            OnJump();
        }
        else if (action == InputActionNames.DODGE && actionState == input_action_state.press)
        {
            OnDodge();
        }
        else if (action == InputActionNames.O && actionState == input_action_state.press)
        {
            OnAttackO();
        }
        else if (action == InputActionNames.SHOWSWORD && actionState == input_action_state.press)
        {
            OnShowSword();
        }
        else if (action == InputActionNames.SWORD_ATTACK_UP && actionState == input_action_state.press)
        {
            OnAttackUp();
        }
    }

    void OnDirectionEvent(Vector2 dir, Vector2 dirRaw, input_action_state inputState)
    {
        //set input dir
        if (blackboard.actorState == actor_action_state.actor_action_state_dodge ||
            blackboard.actorState == actor_action_state.actor_action_state_attack_up)
        {
            return;
        }

        blackboard.moveDir = CalculateMoveDir(dir, inputState);
    }
    Vector3 CalculateMoveDir(Vector2 dir, input_action_state inputState)
    {
        if (inputState == input_action_state.release)
        {
            return Vector3.zero;
        }

        //得到投影向量 为vector到以planeNormal为法向量的平面上。
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(mainCamera.transform.right, Vector3.up).normalized;

        Vector3 forwardDir = forward * dir.y;
        Vector3 rightDir = right * dir.x;
        return (forwardDir + rightDir).normalized;
    }

    //IActorAttackCallback
    public void OnAttackBegin(string animationName)
    {
        //add attack effect
        int nAttackStep = 0;
        string atkName = "";
        if (animationName.Equals("SwordAttack1"))
        {
            nAttackStep = 0;
            atkName = "SwordAttack1";
        }
        else if (animationName.Equals("SwordAttack2"))
        {
            nAttackStep = 1;
            atkName = "SwordAttack2";
        }
        else if (animationName.Equals("SwordAttack3"))
        {
            nAttackStep = 2;
            atkName = "SwordAttack3";
        }
        blackboard.sowrdEffectSocket.PlayEffect(nAttackStep);
        blackboard.attackRange.ActivateWithTime(atkName, 0.6f);
    }
}
