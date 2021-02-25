using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEffectSocket : MonoBehaviour
{
    public GameObject[] swordSlashs;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayEffect(int attackStep)
    {
        if (attackStep >=0 && attackStep < swordSlashs.Length)
        {
            GameObject swordEffect = GameObject.Instantiate(swordSlashs[attackStep]);
            swordEffect.transform.position = transform.position;
            swordEffect.transform.right = transform.parent.forward;
        }
    }
}
