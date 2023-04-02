using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Target;
    public float Dampening;

    private Vector3 velocity;
    private Vector3 offset;
    private Queue<Vector3> locations = new Queue<Vector3>();
    public bool LockX;
    public bool LockY;
    public bool LockZ;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        var x = LockX ? transform.position.x : Target.position.x + offset.x;
        var y = LockY ? transform.position.y : Target.position.y + offset.y;
        var z = LockZ ? transform.position.z : Target.position.z + offset.z;
        var targetLocation = new Vector3(x, y, z);
        if (Dampening == 0)
        {
            transform.position = targetLocation;
            return;
        }
        transform.position = Vector3.SmoothDamp(transform.position, targetLocation, ref velocity, Dampening);
    }
}
