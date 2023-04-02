using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private GameObject container;

    private bool visible = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (visible || LevelManager.Instance.State is LevelManager.GameState.Race or LevelManager.GameState.Pre)
        {
            return;
        }

        visible = true;
        container.SetActive(true);
        if (LevelManager.Instance.State == LevelManager.GameState.Death)
        {
            text.text = "You died";

            return;
        }
        if (LevelManager.Instance.State == LevelManager.GameState.RaceOver)
        {
            var isVictory = LevelManager.Instance.ScoreBoard[0].tag == "Player";
            if (isVictory)
            {
                text.text = "Victory";
                return;
            }
            text.text = "Defeat";
        }
    }
}
