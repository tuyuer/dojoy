using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public SphereCollider attackCollider = null;
    // Start is called before the first frame update
    void Awake()
    {
        attackCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter : " + other.name);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit : " + other.name);
    }
    

}
