using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour, IActorAnimationCallback
{
    protected ActorBlackboard blackboard;
    protected Dictionary<actor_state, ActorAction> actionList = new Dictionary<actor_state, ActorAction>();
    
    // Start is called before the first frame update
    public void Awake()
    {
        blackboard = GetComponent<ActorBlackboard>();
        if (blackboard.swordSocket != null)
        {
            blackboard.swordSocket.gameObject.SetActive(false);
        }

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

    public void Update()
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

    public void OnJump()
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

    public void OnDodge()
    {
        actionList[actor_state.actor_state_dodge].OnEnter();
    }

    public void OnAttackO()
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

    public void ShowSword(bool bShow)
    {
        if (bShow)
        {
            blackboard.showSword = true;
            blackboard.animator.SetBool(AnimatorParameter.ShowSword, true);
            blackboard.swordSocket.gameObject.SetActive(true);
        }
        else
        {
            blackboard.showSword = false;
            blackboard.animator.SetBool(AnimatorParameter.ShowSword, false);
            blackboard.swordSocket.gameObject.SetActive(false);
        }
    }

    public void OnShowSword()
    {
        if (!blackboard.showSword)
        {
            blackboard.showSword = true;
            blackboard.animator.SetBool(AnimatorParameter.ShowSword, true);
            blackboard.swordSocket.gameObject.SetActive(true);
        }
        else
        {
            blackboard.showSword = false;
            blackboard.animator.SetBool(AnimatorParameter.ShowSword, false);
            blackboard.swordSocket.gameObject.SetActive(false);
        }
    }

    public virtual void OnReceiveDamage()
    {
        //Debug.Log("OnReceiveDamage : " + gameObject.name);
    }

    //IActorAnimationCallback implements
    public void OnAnimationEnd(string animationName)
    {
        Debug.Log(animationName + " End");
        if (animationName.Equals(AnimatorParameter.Dodge))
        {
            actionList[actor_state.actor_state_dodge].OnExit();
        }
    }

    //override method for land groud
    public void OnLandGround()
    {
        Debug.Log("OnLandGround");
        actionList[actor_state.actor_state_land].OnEnter();
    }

    public void OnVaultEnd()
    {
        Debug.Log("OnVaultEnd");
        actionList[actor_state.actor_state_vault].OnExit();
    }
}
