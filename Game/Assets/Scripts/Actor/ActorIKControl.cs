using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorIKControl : MonoBehaviour
{
    public bool isActive = false;

    public float lookAtWeight = 1.0f;
    public LayerMask layerMask;         // Select all layers that foot placement applies to.

    [Range(0, 1)]
    public float DistanceToGround;      // Distance from where the foot transform is to the lowest possible position of the foot.

    public Transform lookObj = null;

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
        if (!animator)
        {
            return;
        }

        if (isActive)
        {
            if (lookObj != null)
            {
                animator.SetLookAtWeight(lookAtWeight);
                animator.SetLookAtPosition(lookObj.position);
            }

            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);

            //Left Foot
            Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
            animator.SetLookAtWeight(0);
        }
    }
}
