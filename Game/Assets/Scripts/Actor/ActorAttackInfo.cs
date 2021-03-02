using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum damage_direction
{
    damage_direction_horizontal = 1,
    damage_direction_vertical = 2,
    damage_direction_both
};

[System.Serializable]
public class ActorAttackInfo 
{
    public string attackName;
    public float attackForce;
    public damage_direction damageDirection = damage_direction.damage_direction_horizontal;

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
