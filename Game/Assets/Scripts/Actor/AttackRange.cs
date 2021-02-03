using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [HideInInspector]
    public SphereCollider attackCollider = null;

    // Start is called before the first frame update
    void Awake()
    {
        attackCollider = GetComponent<SphereCollider>();
        attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == TagDef.AttackRangePlayer &&
            other.gameObject.tag == TagDef.Enemy)
        {
            //Player Attack Enemy
            Brain brain = other.gameObject.GetComponent<Brain>();
            brain.OnReceiveDamage();
        }
        else if (gameObject.tag == TagDef.AttackRangeEnemy &&
            other.gameObject.tag == TagDef.Player)
        {
            //Enemy Attack Player
            Brain brain = other.gameObject.GetComponent<Brain>();
            brain.OnReceiveDamage();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TagDef.Enemy)
        {
            Debug.Log("OnTriggerExit : " + other.name);
        }
    }
    
    void EnableRange()
    {
        attackCollider.enabled = true;
    }

    void DisableRange()
    {
        attackCollider.enabled = false;
    }

    public void ActivateWithTime(float startTime, float lastTime = 0.5f)
    {
        CancelInvoke("DisableRange");
        DisableRange();
        EnableRange();
        Invoke("DisableRange", lastTime);
    }
}
