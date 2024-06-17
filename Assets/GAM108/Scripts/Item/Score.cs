using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;

    public static int score = 0;

    private void Start()
    {
        scoreTxt.text = score.ToString();
    }

    public void addScore(int _addScore)
    {
        score += _addScore;
        scoreTxt.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
