using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameManager _gm;
    public Text scoreText;
    public Text livesText;
    public SceneNumber currentScene { get; set; } = SceneNumber.Menu;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
            this.scoreText.text = "Score: " + _gm.score.ToString();
            this.livesText.text = "Lives: " + _gm.lives.ToString(); //idk why but this is throwing infinite null reference errors in unity debugger despite working fine. cbf to look into it more this evening
    }
    public enum SceneNumber{
        Menu,
        Stage1,
        Stage2
    }
    //public void AddScore()
    //{
    //    this.Update();
    //}
    //public void LoseLife()
    //{
    //    lives -= 1;
    //    Update();
    //}
}
