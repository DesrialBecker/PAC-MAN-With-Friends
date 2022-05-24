using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    //public Text highscoreText;
    public int score;
    //public int highscore;

    private void Awake()
    {
        instance = this;
    }
   
    private void Start()
    {
        score = 0;
        DisplayScore(0);
    }
    
    public void AddPoint(int pointValue)
    {
        score += pointValue;
        DisplayScore(score);

    }

    private void DisplayScore(int score)
    {
        scoreText.text = "POINTS: " + score.ToString();
    }
}
