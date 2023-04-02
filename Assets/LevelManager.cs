using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{
    [field: SerializeField] public Kart[] ScoreBoard { get; private set; }
    public PathCreator Road { get; private set; }
    public static LevelManager Instance;

    [SerializeField] private GameObject readyUI;
    [SerializeField] private AudioSource deathGasp; 
    public GameState State { get; private set; } = GameState.Pre;
    public enum GameState
    {
        Pre,
        Race,
        Death,
        RaceOver
    }

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
        if (State == GameState.Race)
        {
            UpdateLeaderBoard();
            if (ScoreBoard.Any(k => k.Location < 63.5f))
            {
                State = GameState.RaceOver;
            }
        }
    }

    void UpdateLeaderBoard()
    {
        Array.Sort(ScoreBoard, (a, b) =>
            a.Location > b.Location ? 1 : -1);
    }

    public void SetDeathState()
    {
        State = GameState.Death;
        deathGasp.Play();
    }
    public void StartRace()
    {
        State = GameState.Race;
        readyUI.SetActive(false);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
