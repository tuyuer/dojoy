using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform followTarget = null;
    // Start is called before the first frame update
    void Start()
    {
        if (followTarget)
        {
            transform.position = followTarget.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget)
        {
            transform.position = followTarget.position;
        }
    }
}
