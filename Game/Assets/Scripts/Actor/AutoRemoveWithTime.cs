using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRemoveWithTime : MonoBehaviour
{
    public float lastTime = 2.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lastTime -= Time.deltaTime;    
        if (lastTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
