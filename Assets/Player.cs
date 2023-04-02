using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private RoadChecker[] roadChecker;
    // Start is called before the first frame update
    void Start()
    {
        roadChecker = GetComponentsInChildren<RoadChecker>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LevelManager.Instance.State == LevelManager.GameState.Race && roadChecker.All(rc => !rc.OnRoad()))
        {
            LevelManager.Instance.SetDeathState();
            Destroy(this.gameObject);
        }
        
    }
}
