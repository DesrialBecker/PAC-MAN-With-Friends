using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static int score;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        Update();
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        Update();
    }
}
