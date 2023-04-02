using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChecker : MonoBehaviour
{
    public bool OnRoad()
    {
        var x = Physics.Raycast(transform.position, Vector3.down, 100f, LayerMask.GetMask("Road"));
        Debug.Log(x);
        return x;

    }
}
