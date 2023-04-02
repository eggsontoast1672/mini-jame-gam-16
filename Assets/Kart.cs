using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour
{
    public float Location = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Location = LevelManager.Instance.Road.path.length;
    }

    private float counter = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (counter++ % 5 != 0) return;
        Location = LevelManager.Instance.Road.path.GetClosestDistanceAlongPath(transform.position);
    }
}
