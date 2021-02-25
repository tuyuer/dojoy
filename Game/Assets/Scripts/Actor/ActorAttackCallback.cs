using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IActorAttackCallback
{
    void OnAttackBegin(string animationName);
}
