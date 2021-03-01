using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [HideInInspector]
    public SphereCollider attackCollider = null;
    public ActorAttackInfo[] attackInfos;

    private string curAtkName;

    // Start is called before the first frame update
    void Awake()
    {
        attackCollider = GetComponent<SphereCollider>();
        attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == TagDef.AttackRangePlayer &&
            other.gameObject.tag == TagDef.Enemy)
        {
            //Player Attack Enemy
            Brain brain = other.gameObject.GetComponent<Brain>();
            brain.OnReceiveDamage(GetCurrentAttackInfo());
        }
        else if (gameObject.tag == TagDef.AttackRangeEnemy &&
            other.gameObject.tag == TagDef.Player)
        {
            //Enemy Attack Player
            Brain brain = other.gameObject.GetComponent<Brain>();
            brain.OnReceiveDamage(GetCurrentAttackInfo());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TagDef.Enemy)
        {
            Debug.Log("OnTriggerExit : " + other.name);
        }
    }
    
    void EnableRange()
    {
        attackCollider.enabled = true;
    }

    void DisableRange()
    {
        attackCollider.enabled = false;
    }

    ActorAttackInfo GetAttackInfoByName(string atkName)
    {
        foreach (ActorAttackInfo atkInfo in attackInfos)
        {
            if (atkInfo.attackName.Equals(atkName))
            {
                return atkInfo;
            }
        }
        return null;
    }

    ActorAttackInfo GetCurrentAttackInfo()
    {
        return GetAttackInfoByName(curAtkName);
    }

    public void ActivateWithTime(string atkName,float startTime, float lastTime = 0.5f)
    {
        curAtkName = atkName;

        string actionDisableRange = "DisableRange";
        CancelInvoke(actionDisableRange);
        DisableRange();
        EnableRange();
        Invoke(actionDisableRange, lastTime);
    }
}
