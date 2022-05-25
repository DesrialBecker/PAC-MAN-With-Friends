using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static int score;
    public Text scoreText;
    public static int lives =5;
    public Text livesText ;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        Update();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (lives <= -1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            this.scoreText.text = "Score: " + score.ToString();
            this.livesText.text = "Lives: " + lives.ToString();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        this.Update();
    }
    //public void LoseLife()
    //{
    //    lives -= 1;
    //    Update();
    //}
}
