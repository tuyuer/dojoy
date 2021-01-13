using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Color originColor = Gizmos.color;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = originColor;
    }
}
