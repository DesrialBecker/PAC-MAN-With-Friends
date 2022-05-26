using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Ghost> ghosts;
    public Pacman pacman;
    public Transform pellets;
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
        if(this.lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
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
    }

    private void GameOver()
    {
        foreach(Ghost ghost in ghosts)
        {
            ghost.gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
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
	}
    public void PowerPelletEaten()
    {
        foreach (Ghost ghost in ghosts)
		{
            ghost.state = Ghost.GhostState.Afraid;
		}
        this.AddScore(PowerPellet.pointValue);
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
