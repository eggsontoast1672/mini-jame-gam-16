using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [field: SerializeField] public Kart[] ScoreBoard { get; private set; }
    public PathCreator Road { get; private set; }
    public static LevelManager Instance;

    // Start is called before the first frame update
    void Awake()
    { 
        Assert.IsNull(Instance);
        Instance = this;
        ScoreBoard = FindObjectsOfType<Kart>();
        Road = FindObjectOfType<PathCreator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Array.Sort(ScoreBoard, (a,b) =>
            a.Location > b.Location ? 1: -1);

    }
}
