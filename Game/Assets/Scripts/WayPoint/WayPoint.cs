using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public way_point_type pointType = way_point_type.way_point_type_patrol;

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
        if (pointType == way_point_type.way_point_type_patrol)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
        Gizmos.color = originColor;
    }
}
