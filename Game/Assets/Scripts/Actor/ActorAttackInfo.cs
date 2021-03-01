using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActorAttackInfo 
{
    public string attackName;
    public float attackForce;
    private Transform attacker;

    public void SetAttacker(Transform attacker)
    {
        this.attacker = attacker;
    }

    public Transform Attacker
    {
        get { return attacker; }
    }
}
