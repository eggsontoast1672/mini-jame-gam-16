using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private float velocity = 0f; 
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float acceleration = 1;

    private PathFollower pathFollower;
    // Start is called before the first frame update
    void Start()
    {
        pathFollower = GetComponentInChildren<PathFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Mathf.Lerp(velocity, maxSpeed, Time.deltaTime * acceleration);
        pathFollower.speed = -velocity;
    }
}
