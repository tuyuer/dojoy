using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBrain : MonoBehaviour
{
    private ClimbAction climbAction;
    
    // Start is called before the first frame update
    void Awake()
    {
        climbAction = GetComponent<ClimbAction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
