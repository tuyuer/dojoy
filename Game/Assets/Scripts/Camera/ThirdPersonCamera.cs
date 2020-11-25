using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform followTarget;
    public float moveSpeed = 2.0f;
    public float rotateSpeed = 3.0f;

    private Vector3 offsetToTarget;

    private Vector3 mousePosition;
    private Vector3 mouseDelta;

    private Vector3 angles;
    private Quaternion originRotation;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        offsetToTarget =  transform.position - followTarget.position;

        mousePosition = Input.mousePosition;
        originRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        mouseDelta = Input.mousePosition - mousePosition;
        mousePosition = Input.mousePosition;

        transform.position = Vector3.Lerp(transform.position, followTarget.position + offsetToTarget, Time.deltaTime * moveSpeed);

        if (Input.GetMouseButton(1))
        {
            angles.x += mouseDelta.y;
            angles.y += mouseDelta.x;
        }
        var yaw = Quaternion.AngleAxis(angles.y, Vector3.up);
        var pitch = Quaternion.AngleAxis(angles.x, Vector3.left);
        transform.localRotation = originRotation * yaw * pitch;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Rotate(mouseX, mouseY);
    }

    void Rotate(float mouseX, float mouseY)
    {

    }
}



//public class ThirdPersonCamera : MonoBehaviour
//{
//    public float distanceAway;
//    public float distanceUp;
//    public float smooth;

//    private Vector3 targetPosition;

//    Transform follow;

//    void Start()
//    {
//        follow = GameObject.FindWithTag("Player").transform;
//    }

//    void LateUpdate()
//    {
//        targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;

//        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

//        transform.LookAt(follow);
//    }
//}