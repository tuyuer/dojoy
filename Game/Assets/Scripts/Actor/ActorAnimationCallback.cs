using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IActorAnimationCallback
{
    void OnAnimationEnd(string animationName);
    void OnLandGround();
    void OnVaultEnd();
}
