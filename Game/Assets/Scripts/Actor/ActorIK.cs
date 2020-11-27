using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorIK : MonoBehaviour
{
    public Transform lookAtTarget = null;

    private Animator animator = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (lookAtTarget != null)
        {
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(lookAtTarget.position);
        }
    }
}
