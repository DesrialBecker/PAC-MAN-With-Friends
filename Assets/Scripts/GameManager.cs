using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Ghost> ghosts;
    [SerializeField] public Pacman pacman;
    public Transform pellets;
    public Transform powerPellets;
    [SerializeField] public Transform respawnPoint;
    public int score { get; set; }
    public int lives { get; set; }
    public int combo { get; set; } = 1;

    public void Start()
    {
        NewGame();
    }
    private void NewGame() {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void Update()
    {
    
    }

    private void NewRound()
    {
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ghosts = FindObjectsOfType<Ghost>().ToList();
        ResetState();
        
    }

    private void ResetState()
    {

        foreach(Ghost ghost in ghosts)
        {
            ghost.gameObject.SetActive(true);
        }
        this.pacman.gameObject.SetActive(true);
        RespawnPacman();
        
    }
    public void RespawnPacman()
    {
        Vector3 position = this.pacman.transform.position;
        position.x = this.respawnPoint.position.x;
        position.y = this.respawnPoint.position.y;
        this.pacman.transform.position = position;
        this.pacman.movement.ResetState();
    }

    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }
    
    private void SetScore(int score)
    {
        this.score = score;
    }
    public void AddScore(int points)
    {
        this.score += points;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten()
    {
        AddScore(Ghost.pointValue * this.combo);
    }

    public void PelletEaten()
	{
        this.AddScore(Pellet.pointValue);
        AllPelletsEaten();
	}

    public void PowerPelletEaten()
    {
        foreach (Ghost ghost in ghosts)
		{
            ghost.state = Ghost.GhostState.Afraid;
		}
        this.AddScore(PowerPellet.pointValue);
        AllPelletsEaten();
    }

    public void LoadNextLevel(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void AllPelletsEaten(){
        if(this.pellets.childCount == 0 && this.powerPellets.childCount==0){
           LoadNextLevel();
        }
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        
        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
            
        }
        else
        {
            GameOver();
        }
    }
   
}
