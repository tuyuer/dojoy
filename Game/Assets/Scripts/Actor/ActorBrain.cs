using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBrain : ActorAnimationCallback
{
    private Camera mainCamera;
    private InputComponent inputComponent;
    private ActorBlackboard blackboard;

    public Dictionary<actor_state, ActorAction> actionList = new Dictionary<actor_state, ActorAction>();

    public Transform swordSocket;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        inputComponent = GetComponent<InputComponent>();
        blackboard = GetComponent<ActorBlackboard>();

        swordSocket.gameObject.SetActive(false);

        actionList.Add(actor_state.actor_state_locomotion, new LocomotionAction());
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
        //input direction
        Vector3 actorSpeed = blackboard.moveDir * GlobalDef.ACTOR_MOVE_SPEED * Time.deltaTime;

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
            if (!blackboard.showSword)
            {
                blackboard.showSword = true;
                blackboard.animator.SetBool(AnimatorParameter.ShowSword, true);
                swordSocket.gameObject.SetActive(true);
            }
            else
            {
                blackboard.showSword = false;
                blackboard.animator.SetBool(AnimatorParameter.ShowSword, false);
                swordSocket.gameObject.SetActive(false);
            }
        }

        //if (action == InputActionNames.O && actionState == input_action_state.press)
        //{
        //    OnAttackO();
        //}
        //else if (action == InputActionNames.X && actionState == input_action_state.press)
        //{
        //    OnAttackX();
        //}
        //else if (action == InputActionNames.DODGE && actionState == input_action_state.press)
        //{
        //    OnDodge();
        //}
        //else if (action == InputActionNames.JUMP && actionState == input_action_state.press)
        //{
        //    OnJump();
        //}
    }

    void OnDirectionEvent(Vector2 dir, Vector2 dirRaw, input_action_state inputState)
    {
        //set input dir
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

    void OnJump()
    {
        Vector3 matchPoint;
        if (CheckVaultable(out matchPoint))
        {
            ArrayList arrayList = new ArrayList();
            arrayList.Add(matchPoint);
            actionList[actor_state.actor_state_vault].OnEnter(arrayList);
        }
        else
        {
            actionList[actor_state.actor_state_jump].OnEnter();
        }
    }

    void OnDodge()
    {
        actionList[actor_state.actor_state_dodge].OnEnter();
    }

    void OnAttackO()
    {
        if (blackboard.showSword)
        {
            actionList[actor_state.actor_state_sword_attack].OnEnter();
        }
        else
        {
            actionList[actor_state.actor_state_punch].OnEnter();
        }
    }

    public override void OnAnimationEnd(string animationName)
    {
        Debug.Log(animationName + " End");
        if (animationName.Equals(AnimatorParameter.Dodge))
        {
            actionList[actor_state.actor_state_dodge].OnExit();
        }
    }

    //override method for land groud
    public override void OnLandGround()
    {
        Debug.Log("OnLandGround");
        actionList[actor_state.actor_state_land].OnEnter();
    }

    public override void OnVaultEnd()
    {
        Debug.Log("OnVaultEnd");
        actionList[actor_state.actor_state_vault].OnExit();
    }

    public bool CheckVaultable(out Vector3 matchPoint)
    {
        matchPoint = Vector3.zero;
        bool bVaultable = false;
        RaycastHit hitInfo;
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), dir, out hitInfo, 10))
        {
            if (hitInfo.collider.tag == TagDef.VaultObject)
            {
                matchPoint = hitInfo.point;
                matchPoint.y = hitInfo.collider.bounds.center.y + hitInfo.collider.bounds.extents.y + 0.35f;

                bVaultable = (hitInfo.distance < 4.0f && hitInfo.distance > 0.5f);
            }
        }
        return bVaultable;
    }
}
