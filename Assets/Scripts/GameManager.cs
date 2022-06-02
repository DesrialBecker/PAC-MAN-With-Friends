using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Transform pellets;
    public Transform powerPellets;
    [SerializeField] public Pacman pacman;
    [SerializeField] public Ghost blinky;
    [SerializeField] public Ghost clyde;
    [SerializeField] public Ghost pinky;
    [SerializeField] public Ghost inky;
    [SerializeField] public Transform pacmanRespawnPoint;
    [SerializeField] public Transform blinkyRespawnPoint;
    [SerializeField] public Transform clydeRespawnPoint;
    [SerializeField] public Transform pinkyRespawnPoint;
    [SerializeField] public Transform inkyRespawnPoint;

    public int lives { get; set; }
    public int score { get; set; }
    public int combo { get; set; }
    public int DifficultyLevel { get; set; } //TODO: abstract to non-numeric levels and a difficulty setter method that sets ghosts intelligence individually

    public void Start()
    {
        NewGame();
    }

    private void NewGame() 
    {
        SetScore(0);
        SetLives(3);
        SetDifficulty(3);
        NewRound();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadNextLevel();
        }

        AllPelletsEaten();
    }

    private void NewRound()
    {
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        foreach (Transform powerPellet in this.powerPellets)
        {
            powerPellet.gameObject.SetActive(true);
        }

        //ghosts = FindObjectsOfType<Ghost>().ToList();
        ResetState();
        ResetCombo();
    }

    private void SpawnFruit()
	{
        
    }

    private void ResetState()
    {

        //This part is if we want to add in enhancement for extra ghosts in the future
        /*
        int id=0;
        
        foreach(Ghost ghost in ghosts)
        {
            int id++;
            ghost.gameObject.SetActive(true);
            RespawnGhost(id);
        }
        */

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }
        this.pacman.gameObject.SetActive(true);
        this.pacman.ResetState();


        RespawnGhost();
        RespawnPacman();

        
        
    }

    /*public void RespawnGhost(int id)
    {
        switch (id) { 
            case 1:
        Vector3 positionBlinky = this.blinky.transform.position;
        this.blinky.gameObject.SetActive(true);
        positionBlinky.x = this.blinkyRespawnPoint.position.x;
        positionBlinky.y = this.blinkyRespawnPoint.position.y;
        this.blinky.transform.position = positionBlinky;
            break;
            case 2:
        Vector3 positionClyde = this.clyde.transform.position;
        this.clyde.gameObject.SetActive(true);
        positionClyde.x = this.clydeRespawnPoint.position.x;
        positionClyde.y = this.clydeRespawnPoint.position.y;
        this.clyde.transform.position = positionClyde;
            break;
            case 3:
        Vector3 positionPinky = this.pinky.transform.position;
        this.pinky.gameObject.SetActive(true);
        positionPinky.x = this.pinkyRespawnPoint.position.x;
        positionPinky.y = this.pinkyRespawnPoint.position.y;
        this.pinky.transform.position = positionPinky;
            break;
            case 4:
        Vector3 positionInky = this.inky.transform.position;
        this.inky.gameObject.SetActive(true);
        positionInky.x = this.inkyRespawnPoint.position.x;
        positionInky.y = this.inkyRespawnPoint.position.y;
        this.inky.transform.position = positionInky;
            break;
        }
    }
    */
     public void RespawnGhost()
     {
        Vector3 positionBlinky = this.blinky.transform.position;
        this.blinky.gameObject.SetActive(true);
        positionBlinky.x = this.blinkyRespawnPoint.position.x;
        positionBlinky.y = this.blinkyRespawnPoint.position.y;
        this.blinky.transform.position = positionBlinky;
        
        Vector3 positionClyde = this.clyde.transform.position;
        this.clyde.gameObject.SetActive(true);
        positionClyde.x = this.clydeRespawnPoint.position.x;
        positionClyde.y = this.clydeRespawnPoint.position.y;
        this.clyde.transform.position = positionClyde;

        Vector3 positionPinky = this.pinky.transform.position;
        this.pinky.gameObject.SetActive(true);
        positionPinky.x = this.pinkyRespawnPoint.position.x;
        positionPinky.y = this.pinkyRespawnPoint.position.y;
        this.pinky.transform.position = positionPinky;
        
        Vector3 positionInky = this.inky.transform.position;
        this.inky.gameObject.SetActive(true);
        positionInky.x = this.inkyRespawnPoint.position.x;
        positionInky.y = this.inkyRespawnPoint.position.y;
        this.inky.transform.position = positionInky;

    }
    

    public void RespawnPacman()
    {
        Vector3 position = this.pacman.transform.position;
        position.x = this.pacmanRespawnPoint.position.x;
        position.y = this.pacmanRespawnPoint.position.y;
        this.pacman.transform.position = position;
        this.pacman._movement.ResetState();

        this.pacman._animator.SetTrigger("Walk");
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

    private void SetDifficulty(int difficulty)
	{
        this.DifficultyLevel = difficulty;
	}

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.PointValue * combo;
        AddScore(ghost.PointValue);
        combo++;
        Invoke(nameof(ResetCombo), 10.0f);
    }
    public void ResetCombo()
    {
        combo = 1;
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
            ghost.Frightened.Enable();
        }
        this.AddScore(PowerPellet.pointValue);

        Invoke(nameof(ChangeGhostStateToChase), 10.0f);
    }

    public void ChangeGhostStateToChase()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.Chase.Enable();
        }
    }

    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(currentScene);
    }

    public void AllPelletsEaten()
    {
      if(this.pellets.childCount == 0 && this.powerPellets.childCount == 0)
        {
            LoadNextLevel();
        }
    }

    public void PacmanEaten()
    {
        this.pacman._animator.SetTrigger("Death");
        this.pacman._movement.speed = 0f;
        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            //RespawnGhost();
            Invoke(nameof(ResetState), 3.0f);

        }
        else
        {
            GameOver();
        }
    }

}
