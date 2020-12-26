using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoints : MonoBehaviour
{
    public float gizmoSize = 0.5f;
    public float sphereScale = 0.1f;
    public bool spherePoint = true;

    private Color sphereColor = new Color(0, 0, 0, 0.1f);

   
    void OnDrawGizmos()
    {
        if (this.enabled == false)
        {
            return;
        }
        Color tmp = Gizmos.color;
        if (spherePoint)
        {
            Gizmos.color = sphereColor;
            Gizmos.DrawSphere(transform.position, gizmoSize * sphereScale);
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * gizmoSize * 1.0f));
        Gizmos.DrawLine(transform.position + (transform.forward * gizmoSize * 1.0f),
            transform.position + (transform.forward * gizmoSize * 0.8f) + (transform.up * gizmoSize * 0.2f));
        Gizmos.DrawLine(transform.position + (transform.forward * gizmoSize * 1.0f),
            transform.position + (transform.forward * gizmoSize * 0.8f) + (transform.up * gizmoSize * -0.2f));
        Gizmos.DrawLine(transform.position + (transform.forward * gizmoSize * 1.0f),
           transform.position + (transform.forward * gizmoSize * 0.8f) + (transform.right * gizmoSize * 0.2f));
        Gizmos.DrawLine(transform.position + (transform.forward * gizmoSize * 1.0f),
           transform.position + (transform.forward * gizmoSize * 0.8f) + (transform.right * gizmoSize * -0.2f));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (transform.up * gizmoSize * 1.0f));
        Gizmos.DrawLine(transform.position + (transform.up * gizmoSize * 1.0f),
            transform.position + (transform.up * gizmoSize * 0.8f) + (transform.forward * gizmoSize * 0.2f));
        Gizmos.DrawLine(transform.position + (transform.up * gizmoSize * 1.0f),
            transform.position + (transform.up * gizmoSize * 0.8f) + (transform.forward * gizmoSize * -0.2f));
        Gizmos.DrawLine(transform.position + (transform.up * gizmoSize * 1.0f),
           transform.position + (transform.up * gizmoSize * 0.8f) + (transform.right * gizmoSize * 0.2f));
        Gizmos.DrawLine(transform.position + (transform.up * gizmoSize * 1.0f),
           transform.position + (transform.up * gizmoSize * 0.8f) + (transform.right * gizmoSize * -0.2f));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (transform.right * gizmoSize * 1.0f));
        Gizmos.DrawLine(transform.position + (transform.right * gizmoSize * 1.0f),
            transform.position + (transform.right * gizmoSize * 0.8f) + (transform.up * gizmoSize * 0.2f));
        Gizmos.DrawLine(transform.position + (transform.right * gizmoSize * 1.0f),
            transform.position + (transform.right * gizmoSize * 0.8f) + (transform.up * gizmoSize * -0.2f));
        Gizmos.DrawLine(transform.position + (transform.right * gizmoSize * 1.0f),
           transform.position + (transform.right * gizmoSize * 0.8f) + (transform.forward * gizmoSize * 0.2f));
        Gizmos.DrawLine(transform.position + (transform.right * gizmoSize * 1.0f),
           transform.position + (transform.right * gizmoSize * 0.8f) + (transform.forward * gizmoSize * -0.2f));

        Gizmos.color = tmp;
    }
}
