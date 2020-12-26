using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAction : MonoBehaviour
{
    protected Animator animator;
    protected CharacterController characterController;

    protected bool applyRootMotion = false;
    protected bool enableCharacterController = true;

    protected bool isEnabled = false;


    protected virtual void OnRunning() { }

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnabled)
        {
            return;
        }

        OnRunning();
    }

    void SetEnabled(bool bNewValue)
    {
        isEnabled = bNewValue;
    }
}
