using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAnimationCallback : MonoBehaviour
{
    public virtual void OnAnimationEnd(string animationName) { }
    public virtual void OnAttackComboBegin(string animationName) { }
    public virtual void OnQuickTurnFinished(string animationName) { }
    public virtual void OnLandGround() { }
    public virtual void OnVaultEnd() { }
}
