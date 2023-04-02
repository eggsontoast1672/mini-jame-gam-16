using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlacementText : MonoBehaviour
{
    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var newText = LevelManager.Instance.ScoreBoard[0].tag == "Player" ? "1st" : "2nd";
        if (text.text == newText) return;
        text.text = newText;
    }
}
