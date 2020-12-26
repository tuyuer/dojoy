using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorIKControl : MonoBehaviour
{
    public bool isActive = false;
    public LayerMask layerMask;         // Select all layers that foot placement applies to.

    [Range(0, 1)]
    public float lookAtWeight;

    [Range(0, 1)]
    public float rightHandWeight;

    [Range(0, 1)]
    public float DistanceToGround;      // Distance from where the foot transform is to the lowest possible position of the foot.

    public Transform lookObj = null;
    public Transform rightHandObj = null;

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

    void OnDrawGizmos()
    {
        if (animator)
        {
            //Gizmos.color = Color.yellow;
            //Gizmos.DrawSphere(animator.GetIKPosition(AvatarIKGoal.LeftFoot), 0.3f);
        }
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

            if (rightHandObj != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
            }

            float leftFootWeight = animator.GetFloat(AnimatorParameter.LeftFootWeight);
            float rightFootWeight = animator.GetFloat(AnimatorParameter.RightFootWeight);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootWeight);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootWeight);

            RaycastHit hit;

            //Left Foot
            // We cast our ray from above the foot in case the current terrain/floor is above the foot position.
            Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceToGround + 2f, layerMask))
            {
                // We're only concerned with objects that are tagged as "Walkable"
                if (hit.transform.tag == "Walkable")
                {
                    Vector3 footPosition = hit.point; // The target foot position is where the raycast hit a walkable object...
                    footPosition.y += DistanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }

            //right foot
            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceToGround + 2f, layerMask))
            {
                // We're only concerned with objects that are tagged as "Walkable"
                if (hit.transform.tag == "Walkable")
                {
                    Vector3 footPosition = hit.point; // The target foot position is where the raycast hit a walkable object...
                    footPosition.y += DistanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
            animator.SetLookAtWeight(0);
        }
    }
}
