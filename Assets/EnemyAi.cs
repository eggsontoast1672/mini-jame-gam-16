using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyAi : MonoBehaviour
{
    private float velocity = 0f; 
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float acceleration = 1;
    [SerializeField] private float letCatchUp = 1;
    [SerializeField] private Kart player;
    private Kart kart;
    private float catchUpMultiplier;

    private PathFollower pathFollower;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(player);
        pathFollower = GetComponentInChildren<PathFollower>();
        kart = GetComponent<Kart>();
    }

    // Update is called once per frame
    void Update()
    {
        pathFollower.speed = -velocity * catchUpMultiplier;

        velocity = Mathf.Lerp(velocity, maxSpeed, Time.deltaTime * acceleration);
        
    }

    void FixedUpdate()
    {
        catchUpMultiplier = 1 - ((player.Location - kart.Location) / letCatchUp);
    }

    public void TakeHit()
    {
        Debug.Log("hit");
        velocity = Mathf.Lerp(velocity, 0, acceleration);
        //todo effect
    }
}
