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

    private PathFollower pathFollower;
    void Start()
    {
        Assert.IsNotNull(player);
        pathFollower = GetComponentInChildren<PathFollower>();
        kart = GetComponent<Kart>();
    }

    void Update()
    {
        if (LevelManager.Instance.State != LevelManager.GameState.Race)
        {
            velocity = Mathf.Lerp(velocity, 0, Time.deltaTime  * 2f);
            pathFollower.speed = velocity;
            return;
        }
        var catchUpMultiplier = (1 - (player.Location - kart.Location) / letCatchUp) * 0.5f;
        pathFollower.speed = -velocity * (catchUpMultiplier + 0.5f);
        velocity = Mathf.Lerp(velocity, maxSpeed, Time.deltaTime * acceleration);
    }

    public void TakeHit()
    {
        velocity = Mathf.Lerp(velocity, 0, acceleration / 4);
    }
}
