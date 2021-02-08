using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    public GameObject[] patrolPoint;

    public Vector3 RandomPatrolPoint()
    {
        int nRandomIndex = Random.Range(0, patrolPoint.Length);
        if (patrolPoint.Length > 0 &&
            nRandomIndex >= 0)
        {
            return patrolPoint[nRandomIndex].transform.position;
        }

        return Vector3.zero;
    }
}
